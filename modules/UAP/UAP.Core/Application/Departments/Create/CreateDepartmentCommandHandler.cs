using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Departments.Create;

internal class CreateDepartmentCommandHandler(IDepartmentRepository repository, IOrganizationRepository organizationRepository, IDepartmentCodeGenerator codeGenerator) : CommandHandler<CreateDepartmentCommand, DepartmentId>
{
    public override async Task<DepartmentId> ExecuteAsync(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var org = await organizationRepository.GetAsync(command.OrganizationId, cancellationToken);

        var exists = await repository.ExistsAsync(m => m.OrganizationId == org.Id && m.Name == command.Name && m.ParentId == command.ParentId, cancellationToken);
        if (exists)
            throw new UAPBusinessException(UAPErrorCode.Department_NameExists);

        var parent = await repository.FindAsync(command.ParentId, cancellationToken);
        if (parent == null)
            throw new UAPBusinessException(UAPErrorCode.Department_ParentNotFound);

        var code = await codeGenerator.GenerateAsync(org, parent, cancellationToken);

        var department = Department.Create(org.Id, command.Name, command.ParentId, code, command.OperatorId!);
        department.SetOrder(command.Order);

        await repository.InsertAsync(department, cancellationToken: cancellationToken);

        return department.Id;
    }
}