using OpenModular.Common.Utils.Models;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganizationTree;

internal class GetOrganizationTreeHandler(
    IOrganizationRepository organizationRepository,
    IDepartmentRepository departmentRepository)
    : IQueryHandler<GetOrganizationTreeQuery, OrganizationTreeDto>
{
    public async Task<OrganizationTreeDto> Handle(GetOrganizationTreeQuery request, CancellationToken cancellationToken)
    {
        var org = await organizationRepository.GetAsync(request.OrganizationId, cancellationToken);
        var departments = await departmentRepository.QueryByCodePrefixAsync(org.Code, cancellationToken);

        var tree = new OrganizationTreeDto(org.Id.Value, org.Name);

        ResolveTree(tree, departments);

        return tree;
    }

    private void ResolveTree(TreeNodeModel<Guid, OrganizationTreeNode> parent, List<Department> departments)
    {
        var children = departments.Where(m => m.ParentId != null && m.ParentId == new DepartmentId(parent.Id)).ToList();
        if (children.Any())
        {
            foreach (var department in children)
            {
                var childNode = new TreeNodeModel<Guid, OrganizationTreeNode>(department.Id.Value, department.Name)
                {
                    Data = new OrganizationTreeNode
                    {
                        Id = department.Id.Value,
                        Name = department.Name,
                        Code = department.Code
                    }
                };

                ResolveTree(childNode, departments);

                parent.Children.Add(childNode);
            }
        }
    }
}