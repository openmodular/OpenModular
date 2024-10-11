using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class AccountRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<Account, AccountId, UAPDbContext>(unitOfWork), IAccountRepository
{
}