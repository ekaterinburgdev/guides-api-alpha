using EkaterinburgDesign.Guides.Api.Database.models;
using Notion.Client;

namespace EkaterinburgDesign.Guides.Api.Repositories.Abstraction;

public interface IPageElementRepository
{
    public Task<PageElement> SaveAsync(IBlock notionElement, ICollection<PageElement>? children, int order);
}