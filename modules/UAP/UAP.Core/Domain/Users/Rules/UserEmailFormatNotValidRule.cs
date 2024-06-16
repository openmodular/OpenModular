using System.Text.RegularExpressions;
using OpenModular.Common.Utils;

namespace OpenModular.Module.UAP.Core.Domain.Users.Rules;

internal record UserEmailFormatNotValidRule(string Email) : UAPBusinessRule(UAPErrorCode.User_EmailFormatNotValid)
{
    public override bool IsBroken()
    {
        Check.NotNullOrWhiteSpace(Email, nameof(Email));

        return !new Regex(RegexExpressionConst.Email).IsMatch(Email);
    }
}