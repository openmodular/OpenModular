using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Application.Users.Get;

namespace OpenModular.Module.UAP.Core.Application.Authentications.RefreshToken;

/// <summary>
/// 刷新令牌命令
/// </summary>
public class RefreshTokenCommand : CommandBase<UserDto>
{
    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }
}