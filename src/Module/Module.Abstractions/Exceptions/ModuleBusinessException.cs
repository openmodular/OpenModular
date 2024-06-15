using OpenModular.Common.Utils.Exceptions;

namespace OpenModular.Module.Abstractions.Exceptions;

/// <summary>
/// 模块业务异常基类
/// </summary>
public abstract class ModuleBusinessException(string moduleCode, Enum errorCode, string? message = null) : ExceptionBase(message)
{
    /// <summary>
    /// 模块编码
    /// </summary>
    public string ModuleCode { get; } = moduleCode;

    /// <summary>
    /// 错误码
    /// </summary>
    public Enum ErrorCode { get; } = errorCode;
}