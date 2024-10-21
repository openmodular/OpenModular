using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Authentications;
using OpenModular.Persistence;
using OpenModular.Persistence.Repositories;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class AuthenticationTokenRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<AuthenticationToken, AccountId, UAPDbContext>(provider), IAuthenticationTokenRepository
{
}