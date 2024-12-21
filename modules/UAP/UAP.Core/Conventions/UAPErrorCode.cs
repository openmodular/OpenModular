using OpenModular.Module.Abstractions.DataAnnotations;

namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// UAP错误码
/// </summary>
[ModuleErrorCode]
public enum UAPErrorCode
{
    /// <summary>
    /// 未知
    /// </summary>
    UnKnown = UAPConstants.ModuleId * 10000,
    /// <summary>
    /// 用户名已存在
    /// </summary>
    Account_UsernameExists,
    /// <summary>
    /// 邮箱已存在
    /// </summary>
    Account_EmailExists,
    /// <summary>
    /// 邮箱已存在
    /// </summary>
    Account_PhoneExists,
    /// <summary>
    /// 邮箱格式不正确
    /// </summary>
    Account_EmailFormatNotValid,
    /// <summary>
    /// 手机号格式不正确
    /// </summary>
    Account_PhoneFormatNotValid,
    /// <summary>
    /// 用户名或手机号或邮箱至少填写一个
    /// </summary>
    Account_UserNameOrEmailOrPhone,
    /// <summary>
    /// 账户已删除
    /// </summary>
    Account_Deleted,
    /// <summary>
    /// 账户已禁用
    /// </summary>
    Account_Disabled,
    /// <summary>
    /// 用户未激活
    /// </summary>
    Account_Inactive,
    /// <summary>
    /// 用户未验证
    /// </summary>
    Account_Unverified,

    /// <summary>
    /// 部门已存在
    /// </summary>
    Department_NameExists,

    /// <summary>
    /// 父部门不存在
    /// </summary>
    Department_ParentNotFound,
    /// <summary>
    /// 密码错误
    /// </summary>
    Auth_PasswordError,
    /// <summary>
    /// 密码不匹配
    /// </summary>
    Auth_PasswordMismatch,
    /// <summary>
    /// 不支持的认证模式
    /// </summary>
    Auth_NotSupportMode,
    /// <summary>
    /// 无效的刷新令牌
    /// </summary>
    Auth_InvalidRefreshToken,
    /// <summary>
    /// 刷新令牌已过期
    /// </summary>
    Auth_RefreshTokenExpired,
    /// <summary>
    /// 用户不存在
    /// </summary>
    Auth_UserNotFound
}