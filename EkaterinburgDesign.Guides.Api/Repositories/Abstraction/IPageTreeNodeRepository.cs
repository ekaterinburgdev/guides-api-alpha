using EkaterinburgDesign.Guides.Api.Database.models;

namespace EkaterinburgDesign.Guides.Api.Repositories.Abstraction;

public interface IPageTreeNodeRepository
{
    public Task<PageTreeNode> SaveAsync(PageTreeNodeData data, PageElement childPageRoot);
}