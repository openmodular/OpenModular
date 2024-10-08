using System.Text.RegularExpressions;
using OpenModular.Common.Utils;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Domain.Users.Rules;

internal record UserEmailFormatNotValidRule(string Email) : UAPBusinessRule(UAPErrorCode.User_EmailFormatNotValid)
{
    public override bool IsBroken()
    {
        if (Email.IsNullOrEmpty())
            return true;

        return !new Regex(RegexExpressionConstants.Email).IsMatch(Email);
    }
}