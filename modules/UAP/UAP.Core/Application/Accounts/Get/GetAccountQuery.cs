using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

/// <summary>
/// 根据指定参数查询账户信息
/// <para>只能指定一个参数</para>
/// </summary>
public class GetAccountQuery : Query<AccountDto?>
{
    /// <summary>
    /// 账户标识
    /// </summary>
    public AccountId? Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }
}