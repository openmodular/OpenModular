namespace OpenModular.DDD.Core.Domain.Exceptions;

/// <summary>
/// 业务规则校验异常
/// </summary>
public class BusinessRuleValidationException(IBusinessRule brokenRule) : Exception(brokenRule.Message)
{
    public IBusinessRule BrokenRule { get; } = brokenRule;

    public string Details { get; } = brokenRule.Message;

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}