using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Authentications;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class AuthenticationTokenRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<AuthenticationToken, AccountId, UAPDbContext>(unitOfWork), IAuthenticationTokenRepository
{
}