using OpenModular.Common.Utils.Paging;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Domain.Organizations.Models;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class OrganizationRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<Organization, OrganizationId, UAPDbContext>(provider), IOrganizationRepository
{
    public async Task<PagedResult<Organization>> PageQueryAsync(OrganizationQueryModel model, Pagination pagination, CancellationToken cancellationToken)
    {
        var query = await GetQueryableAsync();

        return await ToPagedAsync(query, pagination);
    }
}