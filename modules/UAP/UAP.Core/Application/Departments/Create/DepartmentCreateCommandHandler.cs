using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Departments.Create;

internal class DepartmentCreateCommandHandler(IDepartmentRepository repository, IOrganizationRepository organizationRepository, IDepartmentCodeGenerator codeGenerator) : ICommandHandler<DepartmentCreateCommand, DepartmentId>
{
    public async Task<DepartmentId> Handle(DepartmentCreateCommand commond, CancellationToken cancellationToken)
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