using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal class UserValidRule(string? userName, string? email, string? phone) : UAPBusinessRule(UAPErrorCode.Account_UserNameOrEmailOrPhone)
{
    public override bool IsBroken()
    {
        return userName.NotNullOrWhiteSpace() || email.NotNullOrWhiteSpace() || phone.NotNullOrWhiteSpace();
    }
}