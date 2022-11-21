using EkaterinburgDesign.Guides.Api.Database.models;

namespace EkaterinburgDesign.Guides.Api.Repositories;

public record PageTreeNodeData(string Id,
    string Url,
    DateTime LastEdited,
    bool IsShow = true,
    int Order = 0,
    PageTreeNode? Parent = null)
{
    public PageTreeNode? Parent { get; set; } = Parent;
}