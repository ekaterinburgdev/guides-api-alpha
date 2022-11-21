using Newtonsoft.Json;

namespace EkaterinburgDesign.Guides.Api.Database.models;

public class PageElement
{
    public Guid Id { get; set; }

    public string NotionId { get; set; } = default!;

    public string Content { get; set; } = default!;

    public List<PageElement> ChildPageElements { get; set; } = new();

    [JsonIgnore]
    public PageElement? ParentPageElement { get; set; }

    public string Type { get; set; } = default!;

    [JsonIgnore]
    public DateTime LastEdited { get; set; }
    
    [JsonIgnore]
    public int Order { get; set; }
}