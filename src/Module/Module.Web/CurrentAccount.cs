using System.Security.Claims;
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
            var tenantId = _accessor.HttpContext?.User.FindFirst(CustomClaimTypes.TENANT_ID);

            if (tenantId != null && tenantId.Value.NotNull())
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
            var accountId = _accessor.HttpContext?.User.FindFirst(CustomClaimTypes.ACCOUNT_ID);

            if (accountId != null && accountId.Value.NotNull())
            {
                return new AccountId(accountId.Value);
            }

            return null;
        }
    }

    public List<string> Roles
    {
        get
        {
            return _accessor.HttpContext?.User.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value)
                .ToList();
        }
    }
}