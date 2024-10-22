using System.Text.RegularExpressions;
using OpenModular.Common.Utils;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal record AccountEmailFormatNotValidRule(string? Email) : UAPBusinessRule(UAPErrorCode.Account_EmailFormatNotValid)
{
    public override bool IsBroken()
    {
        if (Email.IsNull())
            return true;

        return !new Regex(RegexExpressionConstants.Email).IsMatch(Email);
    }
}