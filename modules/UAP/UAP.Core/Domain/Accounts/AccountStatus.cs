namespace OpenModular.Module.UAP.Core.Domain.Accounts;

/// <summary>
/// 账户状态
/// </summary>
public enum AccountStatus
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