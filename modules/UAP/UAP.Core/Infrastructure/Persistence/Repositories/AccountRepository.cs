using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Persistence;
using OpenModular.Persistence.Repositories;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class AccountRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<Account, AccountId, UAPDbContext>(provider), IAccountRepository
{
}