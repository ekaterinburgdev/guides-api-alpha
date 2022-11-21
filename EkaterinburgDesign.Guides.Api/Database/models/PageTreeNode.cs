namespace EkaterinburgDesign.Guides.Api.Database.models;

public class PageTreeNode
{
    public Guid Id { get; set; }

    public string NotionId { get; set; } = default!;

    public PageElement? ChildPage { get; set; }

    public List<PageTreeNode> ChildNodes { get; set; } = new();

    public PageTreeNode? ParentNode { get; set; }

    public DateTime LastEdited { get; set; }

    public bool Deleted { get; set; }

    public string Url { get; set; } = default!;

    public int Order { get; set; }

    public bool IsShow { get; set; }
}