using OpenModular.DDD.Core.Domain;

namespace OpenModular.Module.UAP.Core.Domain.Users.Rules;

internal record UserCannotActivateWhenAlreadyActivatedRule(UserStatus activatedStatus) : BusinessRuleBase
{
    public override bool IsBroken() => activatedStatus != UserStatus.Inactive;

    public override string Message => "User is already active";
}