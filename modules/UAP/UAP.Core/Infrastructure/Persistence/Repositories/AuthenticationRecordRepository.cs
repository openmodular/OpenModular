using OpenModular.Module.UAP.Core.Domain.Authentications;
using OpenModular.Persistence;
using OpenModular.Persistence.Repositories;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class AuthenticationRecordRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<AuthenticationRecord, int, UAPDbContext>(provider), IAuthenticationRecordRepository
{
}