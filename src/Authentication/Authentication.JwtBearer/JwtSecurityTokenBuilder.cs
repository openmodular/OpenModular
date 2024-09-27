using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenModular.Authentication.Abstractions;

namespace OpenModular.Authentication.JwtBearer;

/// <summary>
/// JWT令牌生成器
/// </summary>
public class JwtSecurityTokenBuilder
{
    private readonly ILogger<JwtSecurityTokenBuilder> _logger;
    private readonly IJwtOptionsProvider _optionsProvider;
    private readonly IJwtSecurityTokenStorage _storage;

    public JwtSecurityTokenBuilder(ILogger<JwtSecurityTokenBuilder> logger, IJwtOptionsProvider optionsProvider, IJwtSecurityTokenStorage storage)
    {
        _logger = logger;
        _optionsProvider = optionsProvider;
        _storage = storage;
    }

    public JwtSecurityToken Build(List<Claim> claims)
    {
        var options = _optionsProvider.Get();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(options.Issuer, options.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(options.Expires), signingCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        _logger.LogInformation("build access_token：{token}", token);

        var jwtCredential = new JwtSecurityToken
        {
            LoginTime = claims.First(m => m.Type == OpenModularClaimTypes.LOGIN_TIME).Value.ToLong(),
            AccessToken = token,
            ExpiresIn = (options.Expires < 0 ? 120 : options.Expires) * 60,
        };

        var refreshToken = Guid.NewGuid().ToString().Replace("-", "");
        jwtCredential.RefreshToken = refreshToken;

        _storage.Save(jwtCredential, claims).Wait();

        return jwtCredential;
    }
}