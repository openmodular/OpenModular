namespace OpenModular.Module.Abstractions.DataAnnotations;

/// <summary>
/// 用于标记模块错误码枚举
/// </summary>
[AttributeUsage(AttributeTargets.Enum)]
public class ModuleErrorCodeAttribute : Attribute;