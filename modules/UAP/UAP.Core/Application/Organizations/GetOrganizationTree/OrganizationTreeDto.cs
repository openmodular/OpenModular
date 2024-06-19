using OpenModular.Common.Utils.Models;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganizationTree;

internal class OrganizationTreeDto(Guid id, string label) : TreeNodeModel<Guid, OrganizationTreeNode>(id, label);

public class OrganizationTreeNode
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }
}