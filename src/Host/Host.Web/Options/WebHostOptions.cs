using OpenModular.Common.Utils;

namespace OpenModular.Host.Web.Options;

/// <summary>
/// Web主机配置
/// </summary>
public class WebHostOptions
{
    public const string Position = $"{OpenModularConstants.Name}:Host";

    /// <summary>
    /// 绑定的地址(默认：http://*:6220)
    /// </summary>
    public string Urls { get; set; } = "http://*:6220";

    /// <summary>
    /// 基础路径
    /// </summary>
    public string? BasePath { get; set; }

    /// <summary>
    /// 指定跨域访问时预检请求的有效期，单位秒，默认30分钟
    /// </summary>
    public int PreflightMaxAge { get; set; } = 1800;

    /// <summary>
    /// 是否启用代理
    /// </summary>
    public bool Proxy { get; set; }

    /// <summary>
    /// 默认目录
    /// </summary>
    public string DefaultDir { get; set; } = "web";

    /// <summary>
    /// 默认页面
    /// </summary>
    public string DefaultPage { get; set; } = "index.html";

    /// <summary>
    /// 禁用数据库迁移
    /// </summary>
    public bool DisableDbMigration { get; set; }

    /// <summary>
    /// 禁用数据种子
    /// </summary>
    public bool DisableDataSeeding { get; set; }
}