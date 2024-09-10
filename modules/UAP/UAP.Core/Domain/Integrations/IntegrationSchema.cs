namespace OpenModular.Module.UAP.Core.Domain.Integrations;

/// <summary>
/// 集成架构
/// </summary>
public sealed class IntegrationSchema
{
    private static readonly List<IntegrationSchema> _schemas = new();

    internal IntegrationSchema(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 集成架构名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 获取所有集成架构列表
    /// </summary>
    public static IEnumerable<IntegrationSchema> Schemas => _schemas;

    /// <summary>
    /// 获取或创建一个集成架构
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IntegrationSchema GetOrCreate(string name)
    {
        var source = new IntegrationSchema(name);
        _schemas.Add(source);
        return source;
    }

    /// <summary>
    /// 获取集成架构，如果不存在则返回null
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IntegrationSchema Find(string name)
    {
        return _schemas.FirstOrDefault(s => s.Name == name);
    }
}