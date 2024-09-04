using OpenModular.Common.Utils.Models;
using OpenModular.DDD.Core.Application.Dto;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetTree;

public record OrganizationTreeDto(Guid Id, string Label) : IDto
{
    public List<TreeNodeModel<Guid, OrganizationTreeNode>> Children { get; set; } = new();
}

public record OrganizationTreeNode(Guid Id, string Name, string Code);
