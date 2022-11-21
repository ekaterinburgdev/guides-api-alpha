using EkaterinburgDesign.Guides.Api.Database;
using EkaterinburgDesign.Guides.Api.Database.models;
using EkaterinburgDesign.Guides.Api.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace EkaterinburgDesign.Guides.Api.Repositories;

public class PageTreeNodeRepository : IPageTreeNodeRepository
{
    private readonly ILogger<PageTreeNodeRepository> log;
    private readonly PostgresContextProvider postgresContextProvider;

    public PageTreeNodeRepository(PostgresContextProvider postgresContextProvider, ILogger<PageTreeNodeRepository> log)
    {
        this.postgresContextProvider = postgresContextProvider;
        this.log = log;
    }

    public async Task<PageTreeNode> SaveAsync(PageTreeNodeData data, PageElement childPageRoot)
    {
        await using var db = postgresContextProvider();

        var sameNodes = await db.PageTreeNodes
            .Where(x => x.NotionId == data.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(node => node.Deleted, true));

        var node = new PageTreeNode
        {
            Deleted = false,
            NotionId = data.Id,
            Order = data.Order,
            Url = data.Url,
            IsShow = data.IsShow,
            LastEdited = data.LastEdited.ToUniversalTime(),
        };

        await db.PageTreeNodes.AddAsync(node);
        await db.SaveChangesAsync();

        node.ChildPage = childPageRoot;
        if (data.Parent is not null)
        {
            node.ParentNode = data.Parent;
        }

        db.PageTreeNodes.Update(node);
        await db.SaveChangesAsync();

        log.LogInformation(node.NotionId);

        return node;
    }
}