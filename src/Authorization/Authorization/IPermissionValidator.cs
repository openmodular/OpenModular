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
    Task<bool> Validate(IDictionary<string, object> routeValues, HttpMethod httpMethod);
}