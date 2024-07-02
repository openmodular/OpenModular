using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Departments;

internal class DefaultDepartmentCodeGenerator(IDepartmentRepository departmentRepository) : IDepartmentCodeGenerator, ITransientDependency
{
    public async Task<string> GenerateAsync(Organization organization, Department parent, CancellationToken cancellationToken = default)
    {
        DepartmentId parentId = null;
        var parentCode = organization.Code;

        if (parent != null)
        {
            parentId = parent.Id;
            parentCode = parent.Code;
        }

        var maxCode = await departmentRepository.GetChildMaxCodeAsync(organization.Id, parentId, cancellationToken);

        var newCode = (int.Parse(maxCode ?? "000") + 1).ToString("D3");

        return parentCode + newCode;
    }
}