using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Users;

/// <summary>
/// 用户仓储接口
/// </summary>
public interface IUserRepository : IRepository<User, UserId>
{
}