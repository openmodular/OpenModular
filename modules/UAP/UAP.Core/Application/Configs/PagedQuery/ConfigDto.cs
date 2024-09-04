using OpenModular.Module.UAP.Core.Domain.Configs;

namespace OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;

public class ConfigDto
{
    public ConfigId Id { get; set; }

    /// <summary>
    /// 模块编码
    /// </summary>
    public string ModuleCode { get; set; }

    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }
}