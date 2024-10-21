using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Accounts;

/// <summary>
/// 账户仓储接口
/// </summary>
public interface IAccountRepository : IRepository<Account, AccountId>;