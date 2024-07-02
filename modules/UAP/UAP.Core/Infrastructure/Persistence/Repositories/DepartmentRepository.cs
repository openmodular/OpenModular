using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class DepartmentRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<Department, DepartmentId, UAPDbContext>(unitOfWork), IDepartmentRepository
{
    public Task<string> GetChildMaxCodeAsync(OrganizationId organizationId, DepartmentId parentId, CancellationToken cancellationToken)
    {
        return DbContext.Departments.Where(m => m.OrganizationId == organizationId && m.ParentId == parentId).MaxAsync(m => m.Code, cancellationToken: cancellationToken);
    }
}