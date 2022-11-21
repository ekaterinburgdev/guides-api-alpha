namespace EkaterinburgDesign.Guides.Api.Notion;

public interface INotionCacher
{
    public Task CachePageAsync(string pageId);
}