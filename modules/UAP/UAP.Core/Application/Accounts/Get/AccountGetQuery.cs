using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

public record AccountGetQuery(AccountId AccountId) : QueryBase<AccountDto>;