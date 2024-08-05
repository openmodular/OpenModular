using OpenModular.DDD.Core.Domain;

namespace OpenModular.Module.UAP.Core.Conventions;

internal abstract record UAPBusinessRule(UAPErrorCode errorCode) : IBusinessRule
{
    public string ModuleCode => UAPConstants.ModuleCode;

    public Enum ErrorCode { get; } = errorCode;

    public abstract bool IsBroken();
}