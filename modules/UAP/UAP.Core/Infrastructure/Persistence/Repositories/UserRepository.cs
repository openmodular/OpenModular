using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class UserRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<User, UserId, UAPDbContext>(unitOfWork), IUserRepository
{
}