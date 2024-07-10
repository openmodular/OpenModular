using Microsoft.EntityFrameworkCore;
using OpenModular.Common.Utils.Paging;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Organizations.Models;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class OrganizationRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<Organization, OrganizationId, UAPDbContext>(unitOfWork), IOrganizationRepository
{
    public async Task<PagedResult<Organization>> PageQueryAsync(OrganizationQueryModel model, Pagination pagination, CancellationToken cancellationToken)
    {
        var query = Db.AsNoTracking();

        return await ToPagedAsync(query, pagination);
    }
}