using Microsoft.AspNetCore.Routing;

namespace OpenModular.Authorization;

/// <summary>
/// 权限验证器
/// </summary>
public interface IPermissionValidator
{
    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="routeValues">路由数据</param>
    /// <param name="httpMethod">请求方式</param>
    /// <returns></returns>
    Task<bool> Validate(RouteValueDictionary routeValues, HttpMethod httpMethod);
}

internal class PermissionValidator : IPermissionValidator
{
    public Task<bool> Validate(RouteValueDictionary routeValues, HttpMethod httpMethod)
    {
        return Task.FromResult(true);
    }
}