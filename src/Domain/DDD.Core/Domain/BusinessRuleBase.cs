namespace OpenModular.DDD.Core.Domain;

public abstract record BusinessRuleBase : IBusinessRule
{
    public abstract Enum ErrorCode { get; }

    public abstract bool IsBroken();
}