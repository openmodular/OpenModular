using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Authentications;

/// <summary>
/// 认证令牌仓储
/// </summary>
public interface IAuthenticationTokenRepository : IRepository<AuthenticationToken, UserId>
{
}