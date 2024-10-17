using Microsoft.EntityFrameworkCore;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Common.Utils.Paging;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.Configs.Models;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class ConfigRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<Config, ConfigId, UAPDbContext>(provider), IConfigRepository
{
    public async Task<PagedResult<Config>> PagedQueryAsync(ConfigPagedQueryModel model, Pagination pagination, CancellationToken cancellationToken)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet.AsNoTracking().WhereNotNull(model.ModuleCode, m => m.ModuleCode == model.ModuleCode);

        return await ToPagedAsync(query, pagination);
    }
}