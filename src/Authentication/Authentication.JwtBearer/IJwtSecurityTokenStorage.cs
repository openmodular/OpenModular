using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Authentication.JwtBearer;

/// <summary>
/// JWT令牌存储器
/// </summary>
public interface IJwtSecurityTokenStorage
{
    /// <summary>
    /// 存储
    /// </summary>
    /// <param name="accountId">用户编号</param>
    /// <param name="client">客户端</param>
    /// <param name="token">令牌</param>
    /// <returns></returns>
    Task SaveAsync(AccountId accountId, AuthenticationClient client, JwtSecurityToken token);
}