namespace OpenModular.DDD.Core.Domain;

/// <summary>
/// 业务规则
/// </summary>
public interface IBusinessRule
{
    bool IsBroken();

    Enum ErrorCode { get; }
}