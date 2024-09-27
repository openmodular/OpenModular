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
    User_UsernameExists,
    /// <summary>
    /// 邮箱已存在
    /// </summary>
    User_EmailExists,
    /// <summary>
    /// 邮箱已存在
    /// </summary>
    User_PhoneExists,
    /// <summary>
    /// 邮箱格式不正确
    /// </summary>
    User_EmailFormatNotValid,
    /// <summary>
    /// 用户已删除
    /// </summary>
    User_Deleted,
    /// <summary>
    /// 用户已禁用
    /// </summary>
    User_Disabled,
    /// <summary>
    /// 用户未激活
    /// </summary>
    User_Inactive,
    /// <summary>
    /// 用户未验证
    /// </summary>
    User_Unverified,

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