namespace OpenModular.Module.UAP.Core.Infrastructure;

/// <summary>
/// 登录验证码服务
/// </summary>
public interface ILoginVerifyCodeService
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <returns></returns>
    Task<LoginVerifyCode> Create();

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="code">验证码</param>
    /// <returns></returns>
    Task<bool> Verify(string id, string code);
}

/// <summary>
/// 登录验证码
/// </summary>
public record LoginVerifyCode(string Id, string Base64);