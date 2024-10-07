using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Domain.Authentications;

/// <summary>
/// 用户JWT凭证
/// </summary>
public class AuthenticationToken : AggregateRoot<UserId>
{
    /// <summary>
    /// 认证客户端
    /// </summary>
    public AuthenticationClient Client { get; }

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 访问令牌有效期
    /// </summary>
    public DateTimeOffset Expires { get; set; }

    public AuthenticationToken()
    {
        //for ef
    }

    private AuthenticationToken(UserId userId, AuthenticationClient client) : base(userId)
    {
        Client = client;
    }

    /// <summary>
    /// 创建用户认证令牌
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    public static AuthenticationToken Create(UserId userId, AuthenticationClient client)
    {
        return new AuthenticationToken(userId, client);
    }
}