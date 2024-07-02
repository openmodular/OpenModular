using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Departments.CreateDepartment;

internal class CreateDepartmentCommandHandler(IDepartmentRepository repository, IOrganizationRepository organizationRepository, IDepartmentCodeGenerator codeGenerator) : ICommandHandler<CreateDepartmentCommand, DepartmentId>
{
    public async Task<DepartmentId> Handle(CreateDepartmentCommand commond, CancellationToken cancellationToken)
    {
        var org = await organizationRepository.GetAsync(commond.OrganizationId, cancellationToken);

        var exists = await repository.ExistsAsync(m => m.OrganizationId == org.Id && m.Name == commond.Name && m.ParentId == commond.ParentId, cancellationToken);
        if (exists)
            throw new UAPBusinessException(UAPErrorCode.Department_NameExists);

        var parent = await repository.FindAsync(commond.ParentId, cancellationToken);
        if (parent == null)
            throw new UAPBusinessException(UAPErrorCode.Department_ParentNotFound);

        var code = await codeGenerator.GenerateAsync(org, parent, cancellationToken);

        var department = Department.Create(org.Id, commond.Name, commond.ParentId, code, commond.CreatedBy);
        department.SetOrder(commond.Order);

        await repository.InsertAsync(department, cancellationToken);

        return department.Id;
    }
}