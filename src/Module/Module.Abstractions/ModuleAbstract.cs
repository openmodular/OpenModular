namespace OpenModular.Module.Abstractions;

/// <summary>
/// 用于定义模块的抽象类
/// </summary>
public abstract class ModuleAbstract : IModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">模块唯一标识</param>
    /// <param name="code">模块唯一编码</param>
    protected ModuleAbstract(int id, string code)
    {
        Id = id;
        Code = code;
        Version = GetType().Assembly.GetName().Version?.ToString() ?? "1.0.0";
    }

    public int Id { get; }

    public string Code { get; }

    public string Version { get; }
}