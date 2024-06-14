namespace OpenModular.DDD.Core.Domain;

public abstract record BusinessRuleBase : IBusinessRule
{
    public abstract string Message { get; }

    public abstract bool IsBroken();
}