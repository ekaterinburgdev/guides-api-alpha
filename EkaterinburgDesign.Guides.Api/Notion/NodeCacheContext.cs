using EkaterinburgDesign.Guides.Api.Repositories;

namespace EkaterinburgDesign.Guides.Api.Notion;

public class NodeCacheContext
{
    public NodeCacheContext(string currentNodeId) => CurrentNodeId = currentNodeId;

    public string CurrentNodeId { get; }

    public ICollection<PageTreeNodeData> ChildNodesData { get; set; } = new List<PageTreeNodeData>();
}