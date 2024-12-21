using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal class AccountCannotActivateWhenAlreadyActivatedRule(AccountStatus activatedStatus) : UAPBusinessRule(UAPErrorCode.Account_EmailFormatNotValid)
{
    public override bool IsBroken() => activatedStatus != AccountStatus.Inactive;
}