using System.Text.RegularExpressions;
using OpenModular.Common.Utils;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal record AccountEmailFormatNotValidRule(string Email) : UAPBusinessRule(UAPErrorCode.Account_EmailFormatNotValid)
{
    public override bool IsBroken()
    {
        if (Email.IsNullOrEmpty())
            return true;

        return !new Regex(RegexExpressionConstants.Email).IsMatch(Email);
    }
}