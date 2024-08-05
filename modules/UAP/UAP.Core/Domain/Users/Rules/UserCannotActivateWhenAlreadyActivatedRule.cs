using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Users.Rules;

internal record UserCannotActivateWhenAlreadyActivatedRule(UserStatus ActivatedStatus) : UAPBusinessRule(UAPErrorCode.User_EmailFormatNotValid)
{
    public override bool IsBroken() => ActivatedStatus != UserStatus.Inactive;
}