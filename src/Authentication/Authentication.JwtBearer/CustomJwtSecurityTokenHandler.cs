using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Common.Utils;

namespace OpenModular.Authentication.JwtBearer;

internal class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler
{
    public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        using var scope = GlobalServiceProvider.CreateScope();

        var provider = scope.ServiceProvider.GetRequiredService<IJwtOptionsProvider>();

        var options = provider.GetAsync().GetAwaiter().GetResult();

        validationParameters.ValidIssuer = options.Issuer;
        validationParameters.ValidAudience = options.Audience;
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));

        return base.ValidateToken(token, validationParameters, out validatedToken);
    }
}