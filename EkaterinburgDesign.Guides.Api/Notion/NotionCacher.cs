using EkaterinburgDesign.Guides.Api.Database.models;
using EkaterinburgDesign.Guides.Api.Repositories;
using EkaterinburgDesign.Guides.Api.Repositories.Abstraction;
using Notion.Client;

namespace EkaterinburgDesign.Guides.Api.Notion;

public class NotionCacher : INotionCacher
{
    private readonly INotionClient notion;
    private readonly IPageElementRepository pageElementRepository;
    private readonly IPageTreeNodeRepository pageTreeNodeRepository;
    private ILogger<NotionCacher> log;

    private const string RootPageUrl = "root";

    public NotionCacher(INotionClient notion, IPageElementRepository pageElementRepository,
        IPageTreeNodeRepository pageTreeNodeRepository, ILogger<NotionCacher> log)
    {
        this.notion = notion;
        this.pageElementRepository = pageElementRepository;
        this.pageTreeNodeRepository = pageTreeNodeRepository;
        this.log = log;
    }

    public async Task CachePageAsync(string pageId)
    {
        var pageTreeNodeData = new PageTreeNodeData(pageId, RootPageUrl, DateTime.Now);

        try
        {
            await CachePageTreeNodeAsync(pageTreeNodeData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task CachePageTreeNodeAsync(PageTreeNodeData data)
    {
        var context = new NodeCacheContext(data.Id);

        var rootElement = await notion.Blocks.RetrieveAsync(data.Id);
        var childElements = await CacheChildElementsAsync(data.Id, context);

        var page = await pageElementRepository.SaveAsync(rootElement, childElements, 0);

        var node = await pageTreeNodeRepository.SaveAsync(data, page);

        foreach (var childNode in context.ChildNodesData)
        {
            childNode.Parent = node;

            try
            {
                await CachePageTreeNodeAsync(childNode);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
        }
    }

    private async Task<ICollection<PageElement>> CacheChildElementsAsync(string elementId, NodeCacheContext context)
    {
        var pageChildren = await notion.Blocks.RetrieveChildrenAsync(elementId);
        var result = new List<PageElement>();

        var i = 0;

        foreach (var element in pageChildren.Results)
        {
            if (element.Type is BlockType.ChildDatabase)
            {
                await UpdateContextChildNodesAsync(element.Id, context);
                continue;
            }

            var ownChildren = element.HasChildren
                ? await CacheChildElementsAsync(element.Id, context)
                : null;

            var pageElement = await pageElementRepository.SaveAsync(element, ownChildren, i);

            result.Add(pageElement);

            i++;
        }

        return result;
    }

    private async Task UpdateContextChildNodesAsync(string notionDbId, NodeCacheContext context)
    {
        var queryParameters = new DatabasesQueryParameters();

        var items = await notion.Databases.QueryAsync(notionDbId, queryParameters);

        foreach (var item in items.Results)
        {
            var properties = new Dictionary<string, PropertyValue>(item.Properties, StringComparer.OrdinalIgnoreCase);

            var hasPublished = properties.TryGetValue("published", out var publishedValue);
            var published = hasPublished ? publishedValue as CheckboxPropertyValue : null;

            if (published?.Checkbox is not null && !published.Checkbox)
            {
                continue;
            }

            var hasUrl = properties.TryGetValue("pageUrl", out var urlValue);
            var url = hasUrl ? urlValue as UrlPropertyValue : null;

            var hasOrder = properties.TryGetValue("order", out var orderValue);
            var order = hasOrder ? orderValue as NumberPropertyValue : null;

            var data = new PageTreeNodeData(item.Id,
                url?.Url ?? throw new InvalidOperationException(),
                DateTime.Now,
                published?.Checkbox ?? true,
                order?.Number is not null ? (int)Math.Ceiling(order.Number.Value) : default);

            context.ChildNodesData.Add(data);
        }
    }
}