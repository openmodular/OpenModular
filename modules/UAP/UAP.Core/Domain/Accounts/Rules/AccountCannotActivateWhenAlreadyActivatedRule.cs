using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal record AccountCannotActivateWhenAlreadyActivatedRule(AccountStatus ActivatedStatus) : UAPBusinessRule(UAPErrorCode.Account_EmailFormatNotValid)
{
    public override bool IsBroken() => ActivatedStatus != AccountStatus.Inactive;
}