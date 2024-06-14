namespace OpenModular.Module.UAP.Core.Domain.Users;

/// <summary>
/// 用户状态
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// 未验证
    /// </summary>
    Unverified,

    /// <summary>
    /// 未激活
    /// </summary>
    Inactive,

    /// <summary>
    /// 已启用
    /// </summary>
    Enabled,

    /// <summary>
    /// 已禁用
    /// </summary>
    Disabled,

    /// <summary>
    /// 已删除
    /// </summary>
    Deleted,
}