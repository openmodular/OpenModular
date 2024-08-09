namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// UAP配置
/// </summary>
public class UAPConfig
{
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
    /// 是否启用图片验证码
    /// </summary>
    public ImageCaptchaConfig ImageCaptcha { get; set; } = new();
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