using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;

namespace OpenModular.Module.UAP.Core.Application.Authentications.RefreshToken;

/// <summary>
/// 刷新令牌命令
/// </summary>
public class RefreshTokenCommand : Command<AccountDto>
{
    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 认证客户端
    /// </summary>
    public AuthenticationClient Client { get; set; }
}