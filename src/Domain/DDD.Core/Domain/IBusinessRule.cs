namespace OpenModular.DDD.Core.Domain;

/// <summary>
/// 业务规则
/// </summary>
public interface IBusinessRule
{
    string ModuleCode { get; }

    Enum ErrorCode { get; }

    bool IsBroken();
}