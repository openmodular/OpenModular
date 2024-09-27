namespace OpenModular.Authentication.JwtBearer;

/// <summary>
/// Json Web Security Token
/// </summary>
[Serializable]
public class JwtSecurityToken
{
    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 访问令牌有效期(单位：秒)
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 登录时间戳
    /// </summary>
    public long LoginTime { get; set; }
}