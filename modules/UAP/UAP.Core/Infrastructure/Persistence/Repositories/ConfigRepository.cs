using Microsoft.EntityFrameworkCore;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Common.Utils.Paging;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.Configs.Models;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class ConfigRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<Config, ConfigId, UAPDbContext>(unitOfWork), IConfigRepository
{
    public Task<PagedResult<Config>> PagedQueryAsync(ConfigPagedQueryModel model, Pagination pagination, CancellationToken cancellationToken)
    {
        var query = Db.AsNoTracking();
        query = query.WhereNotNull(model.ModuleCode, m => m.ModuleCode == model.ModuleCode);

        return ToPagedAsync(query, pagination);
    }
}