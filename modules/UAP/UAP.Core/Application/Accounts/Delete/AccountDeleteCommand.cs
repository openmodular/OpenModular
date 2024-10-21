using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Delete;

/// <summary>
/// 账户删除命令
/// </summary>
public class AccountDeleteCommand : Command
{
    public AccountDeleteCommand(AccountId id)
    {
        Id = id;
    }

    /// <summary>
    /// 账户标识
    /// </summary>
    public AccountId Id { get; set; }
}