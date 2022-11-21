using EkaterinburgDesign.Guides.Api.Database;
using EkaterinburgDesign.Guides.Api.Database.models;
using EkaterinburgDesign.Guides.Api.Repositories.Abstraction;
using Newtonsoft.Json;
using Notion.Client;

namespace EkaterinburgDesign.Guides.Api.Repositories;

public class PageElementRepository : IPageElementRepository
{
    private readonly PostgresContextProvider postgresContextProvider;
    private readonly ILogger<PageElementRepository> log;

    public PageElementRepository(PostgresContextProvider postgresContextProvider, ILogger<PageElementRepository> log)
    {
        this.postgresContextProvider = postgresContextProvider;
        this.log = log;
    }

    public async Task<PageElement> SaveAsync(IBlock notionElement, ICollection<PageElement>? children, int order)
    {
        var content = JsonConvert.SerializeObject(notionElement);

        var element = new PageElement
        {
            NotionId = notionElement.Id,
            Content = content,
            Type = notionElement.Type.ToString(),
            LastEdited = notionElement.LastEditedTime,
            Order = order
        };

        await using var db = postgresContextProvider();

        await db.AddAsync(element);

        await db.SaveChangesAsync();

        if (children is null)
        {
            return element;
        }

        element.ChildPageElements.AddRange(children);
        db.Update(element);
        await db.SaveChangesAsync();

        log.LogInformation(element.NotionId);

        return element;
    }
}