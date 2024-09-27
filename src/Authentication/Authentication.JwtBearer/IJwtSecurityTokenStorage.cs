using System.Security.Claims;

namespace OpenModular.Authentication.JwtBearer;

/// <summary>
/// JWT令牌存储器
/// </summary>
public interface IJwtSecurityTokenStorage
{
    /// <summary>
    /// 存储
    /// </summary>
    /// <param name="model"></param>
    /// <param name="claims"></param>
    /// <returns></returns>
    Task Save(JwtSecurityToken model, List<Claim> claims);

    /// <summary>
    /// 检测刷新令牌并返回账户编号
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="platform"></param>
    /// <returns></returns>
    Task<Guid> CheckRefreshToken(string refreshToken, int platform);
}