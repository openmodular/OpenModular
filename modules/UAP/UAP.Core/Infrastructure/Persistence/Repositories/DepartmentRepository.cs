using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Persistence;
using OpenModular.Persistence.Repositories;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class DepartmentRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<Department, DepartmentId, UAPDbContext>(provider), IDepartmentRepository
{
    public async Task<string> GetChildMaxCodeAsync(OrganizationId organizationId, DepartmentId parentId, CancellationToken cancellationToken)
    {
        var query = await GetQueryableAsync();
        return await query.Where(m => m.OrganizationId == organizationId && m.ParentId == parentId).MaxAsync(m => m.Code, cancellationToken: cancellationToken);
    }
}