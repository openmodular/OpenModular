using Microsoft.AspNetCore.Mvc;
using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.Web;

/// <summary>
/// 控制器抽象基类
/// </summary>
[ApiController]
[Route("api/[area]/[controller]/[action]")]
public abstract class ControllerAbstract : ControllerBase
{
    /// <summary>
    /// 租户标识
    /// </summary>
    public TenantId TenantId
    {
        get
        {
            var tenantId = User.FindFirst(OpenModularClaimTypes.TENANT_ID);

            if (tenantId != null && tenantId.Value.NotNull())
            {
                return new TenantId(tenantId.Value);
            }

            return null;
        }
    }

    /// <summary>
    /// 当前登录用户标识
    /// </summary>
    public UserId UserId
    {
        get
        {
            var userId = User.FindFirst(OpenModularClaimTypes.USER_ID);

            if (userId != null && userId.Value.NotNull())
            {
                return new UserId(userId.Value);
            }

            return null;
        }
    }

    /// <summary>
    /// 获取当前用户IP(包含IPv和IPv6)
    /// </summary>
    public string IP => HttpContext.Connection.RemoteIpAddress?.ToString();

    /// <summary>
    /// 获取当前用户IPv4
    /// </summary>
    public string IPv4 => HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

    /// <summary>
    /// 获取当前用户IPv6
    /// </summary>
    public string IPv6 => HttpContext.Connection.RemoteIpAddress?.MapToIPv6().ToString();

    /// <summary>
    /// 登录时间
    /// </summary>
    public long LoginTime
    {
        get
        {
            var ty = User.FindFirst(OpenModularClaimTypes.LOGIN_TIME);

            if (ty != null && ty.Value.NotNull())
            {
                return ty.Value.ToLong();
            }

            return 0L;
        }
    }

    /// <summary>
    /// User-Agent
    /// </summary>
    public string UserAgent => HttpContext.Request.Headers["User-Agent"];
}