using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// 统一认证平台缓存键
/// </summary>
public sealed class UAPCacheKeys
{
    /// <summary>
    /// 验证码缓存键
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string Captcha(string id) => $"VerifyCode:{id}";

    public static string User(UserId userId) => $"User:{userId}";
}