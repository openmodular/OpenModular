using Mapster;
using Microsoft.AspNetCore.Routing;

namespace OpenModular.Module.Api;

/// <summary>
/// 用于定义端点的接口
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// 对象映射配置
    /// </summary>
    /// <param name="config"></param>
    void Mapping(TypeAdapterConfig config);

    /// <summary>
    /// 路由映射
    /// </summary>
    /// <param name="group">路由组构造器</param>
    /// <param name="app">应用构造器</param>
    void RouteMap(RouteGroupBuilder group, IEndpointRouteBuilder app);
}