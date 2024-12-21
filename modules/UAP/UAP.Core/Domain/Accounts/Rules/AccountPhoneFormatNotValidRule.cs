using System.Text.RegularExpressions;
using OpenModular.Common.Utils;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

internal class AccountPhoneFormatNotValidRule(string? phone) : UAPBusinessRule(UAPErrorCode.Account_PhoneFormatNotValid)
{
    public override bool IsBroken()
    {
        if (phone.IsNullOrWhiteSpace())
            return true;

        return !new Regex(RegexExpressionConstants.Phone).IsMatch(phone);
    }
}