using OpenModular.DDD.Core.Domain;

namespace OpenModular.Module.UAP.Core;

internal abstract record UAPBusinessRule(UAPErrorCode errorCode) : IBusinessRule
{
    public abstract bool IsBroken();

    public Enum ErrorCode { get; } = errorCode;
}