using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OpenModular.Common.Utils;

namespace OpenModular.Authentication.JwtBearer;

internal class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler
{
    public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        var provider = GlobalServiceProvider.GetRequiredService<IJwtOptionsProvider>();

        var options = provider.Get();

        validationParameters.ValidIssuer = options.Issuer;
        validationParameters.ValidAudience = options.Audience;
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));

        return base.ValidateToken(token, validationParameters, out validatedToken);
    }
}