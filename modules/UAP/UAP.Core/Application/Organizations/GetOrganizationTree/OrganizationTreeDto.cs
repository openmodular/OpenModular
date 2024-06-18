using OpenModular.Common.Utils.Models;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganizationTree;

internal class OrganizationTreeDto : TreeNodeModel<OrganizationId, OrganizationTreeNode>
{
    public OrganizationTreeDto(OrganizationId id, string label) : base(id, label)
    {
    }
}

public class OrganizationTreeNode
{
    public string Name { get; set; }

    public string Code { get; set; }
}