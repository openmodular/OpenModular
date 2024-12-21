using System.Text.RegularExpressions;
using OpenModular.Common.Utils;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal class AccountEmailFormatNotValidRule(string? email) : UAPBusinessRule(UAPErrorCode.Account_EmailFormatNotValid)
{
    public override bool IsBroken()
    {
        if (email.IsNullOrWhiteSpace())
            return true;

        return !new Regex(RegexExpressionConstants.Email).IsMatch(email);
    }
}