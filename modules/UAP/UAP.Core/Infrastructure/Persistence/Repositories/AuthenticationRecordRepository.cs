using OpenModular.Module.UAP.Core.Domain.Authentications;
using OpenModular.DDD.Core.Uow;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class AuthenticationRecordRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<AuthenticationRecord, int, UAPDbContext>(unitOfWork), IAuthenticationRecordRepository
{
}