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
    /// 用户已删除
    /// </summary>
    Auth_UserDeleted,
    /// <summary>
    /// 用户已禁用
    /// </summary>
    Auth_UserDisabled,
    /// <summary>
    /// 密码不匹配
    /// </summary>
    Auth_PasswordMismatch,
    /// <summary>
    /// 不支持的认证模式
    /// </summary>
    Auth_NotSupportMode,
}