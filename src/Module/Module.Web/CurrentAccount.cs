using Microsoft.AspNetCore.Http;
using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.Web;

internal class CurrentAccount : ICurrentAccount
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentAccount(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public TenantId? TenantId
    {
        get
        {
            var tenantId = _accessor.HttpContext?.User.FindFirst(ClaimTypes.TENANT_ID);

            if (tenantId != null && tenantId.Value.IsNotNullOrWhiteSpace())
            {
                return new TenantId(tenantId.Value);
            }

            return null;
        }
    }

    public AccountId? AccountId
    {
        get
        {
            var accountId = _accessor.HttpContext?.User.FindFirst(ClaimTypes.ACCOUNT_ID);

            if (accountId != null && accountId.Value.IsNotNullOrWhiteSpace())
            {
                return new AccountId(accountId.Value);
            }

            return null;
        }
    }
}