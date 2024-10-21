using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;

namespace OpenModular.Module.UAP.Core.Application.Accounts.GetByPhone;

/// <summary>
/// 根据手机号查询账户信息
/// </summary>
public class AccountGetByPhoneQuery : Query<AccountDto>
{
    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }
}