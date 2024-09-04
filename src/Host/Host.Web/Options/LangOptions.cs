using OpenModular.Common.Utils;

namespace OpenModular.Host.Web.Options;

/// <summary>
/// 多语言配置
/// </summary>
public class LangOptions
{
    public const string Position = $"{OpenModularConstants.Name}:Lang";

    /// <summary>
    /// 默认语言
    /// </summary>
    public string DefaultLang { get; set; } = "zh-CN";

    /// <summary>
    /// 支持的语言
    /// </summary>
    public string[] Supported { get; set; } = { "zh-CN", "en-US" };
}