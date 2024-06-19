using OpenModular.Common.Utils.Models;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganizationTree;

public record OrganizationTreeDto(Guid Id, string Label)
{
    public List<TreeNodeModel<Guid, OrganizationTreeNode>> Children { get; set; } = new();
}

public record OrganizationTreeNode(Guid Id, string Name, string Code);
