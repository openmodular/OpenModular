using System.Text.RegularExpressions;
using OpenModular.Common.Utils;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal record AccountPhoneFormatNotValidRule(string? Phone) : UAPBusinessRule(UAPErrorCode.Account_PhoneFormatNotValid)
{
    public override bool IsBroken()
    {
        if (Phone.IsNull())
            return true;

        return !new Regex(RegexExpressionConstants.Phone).IsMatch(Phone);
    }
}