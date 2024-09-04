using OpenModular.Common.Utils.Models;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetTree;

internal class OrganizationTreeGetHandler(
    IOrganizationRepository organizationRepository,
    IDepartmentRepository departmentRepository)
    : IQueryHandler<OrganizationTreeGetQuery, OrganizationTreeDto>
{
    public async Task<OrganizationTreeDto> Handle(OrganizationTreeGetQuery request, CancellationToken cancellationToken)
    {
        var org = await organizationRepository.GetAsync(request.OrganizationId, cancellationToken);
        var departments = await departmentRepository.GetListAsync(m => m.Code.StartsWith(org.Code), cancellationToken);

        var tree = new OrganizationTreeDto(org.Id.Value, org.Name);

        var rootDepartments = departments.Where(m => m.ParentId == null);
        foreach (var department in rootDepartments)
        {
            var rootNode = new TreeNodeModel<Guid, OrganizationTreeNode>(department.Id.Value, department.Name)
            {
                Data = new OrganizationTreeNode(department.Id.Value, department.Name, department.Code)
            };
            ResolveTree(rootNode, departments);

            tree.Children.Add(rootNode);
        }

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
                    Data = new OrganizationTreeNode(department.Id.Value, department.Name, department.Code)
                };

                ResolveTree(childNode, departments);

                parent.Children.Add(childNode);
            }
        }
    }
}