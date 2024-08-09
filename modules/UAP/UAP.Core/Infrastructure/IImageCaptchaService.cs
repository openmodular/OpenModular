namespace OpenModular.Module.UAP.Core.Infrastructure;

/// <summary>
/// 验证码服务
/// </summary>
public interface IImageCaptchaService
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <returns></returns>
    Task<ImageCaptcha> CreateAsync();

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="code">验证码</param>
    /// <returns></returns>
    Task<bool> VerifyAsync(string id, string code);
}

/// <summary>
/// 登录验证码
/// </summary>
public record ImageCaptcha(string Id, string Base64);