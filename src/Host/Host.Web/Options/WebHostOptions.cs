namespace OpenModular.Host.Web.Options;

/// <summary>
/// Web主机配置
/// </summary>
public class WebHostOptions
{
    /// <summary>
    /// 绑定的地址(默认：http://*:6220)
    /// </summary>
    public string Urls { get; set; } = "http://*:6220";

    /// <summary>
    /// 基础路径
    /// </summary>
    public string BasePath { get; set; }

    /// <summary>
    /// 指定跨域访问时预检请求的有效期，单位秒，默认30分钟
    /// </summary>
    public int PreflightMaxAge { get; set; } = 1800;

    /// <summary>
    /// 是否启用代理
    /// </summary>
    public bool Proxy { get; set; }
}