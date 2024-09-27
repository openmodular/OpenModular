using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Domain.Authentications;

/// <summary>
/// 用户JWT凭证
/// </summary>
public class AuthenticationToken : AggregateRoot<UserId>
{
    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; }

    /// <summary>
    /// 访问令牌有效期
    /// </summary>
    public DateTimeOffset Expires { get; }

    public AuthenticationToken()
    {
        //for ef
    }

    public AuthenticationToken(UserId userId, string accessToken, string refreshToken, DateTime expires) : base(userId)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Expires = expires;
    }
}