using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

/// <summary>
/// 配置编号
/// </summary>
public class ConfigId : TypedIdValueBase
{
    public ConfigId()
    {

    }

    public ConfigId(string id) : base(id)
    {

    }

    public ConfigId(Guid id) : base(id)
    {

    }
}