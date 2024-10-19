using OpenModular.Configuration.Abstractions;

namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// UAP配置
/// </summary>
public class UAPConfig : ConfigAbstract
{
    public UAPConfig() : base(UAPConstants.ModuleCode)
    {
    }

    /// <summary>
    /// 认证配置
    /// </summary>
    public AuthenticationConfig Authentication { get; set; } = new();
}

/// <summary>
/// 认证配置
/// </summary>
public class AuthenticationConfig
{
    /// <summary>
    /// 图片验证码配置
    /// </summary>
    public ImageCaptchaConfig ImageCaptcha { get; set; } = new();

    /// <summary>
    /// Jwt配置
    /// </summary>
    public JwtConfig? Jwt { get; set; }
}

/// <summary>
/// 图形验证码配置
/// </summary>
public class ImageCaptchaConfig
{
    /// <summary>
    /// 是否启用图形验证码校验
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// 验证码长度
    /// </summary>
    public int Length { get; set; } = 4;

    /// <summary>
    /// 验证码有效期(分钟)
    /// </summary>
    public int Expire { get; set; } = 5;
}

/// <summary>
/// Json Web Token配置
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// 加密密钥
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 发行人
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 消费者
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 令牌有效期(分钟，默认120)
    /// </summary>
    public int Expires { get; set; } = 120;

    /// <summary>
    /// 刷新令牌有效期(单位：天，默认7)
    /// </summary>
    public int RefreshTokenExpires { get; set; } = 7;
}