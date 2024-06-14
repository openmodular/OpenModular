using OpenModular.DDD.Core.Domain;
using System.Text.RegularExpressions;
using OpenModular.Common.Utils;

namespace OpenModular.Module.UAP.Core.Domain.Users.Rules;

public record UserEmailFormatNotValidRule(string email) : BusinessRuleBase
{
    public override bool IsBroken()
    {
        Check.NotNullOrWhiteSpace(email, nameof(email));

        return !new Regex(RegexExpressionConst.Email).IsMatch(email);
    }

    public override string Message => "The user email is not a valid mailbox format";
}