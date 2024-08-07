using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenModular.Authentication.Abstractions;

namespace OpenModular.Authentication.JwtBearer;

/// <summary>
/// JWT凭证生成器
/// </summary>
public class JwtCredentialBuilder : ICredentialBuilder
{
    private readonly IOptionsMonitor<JwtOptions> _options;
    private readonly ILogger<JwtCredentialBuilder> _logger;

    public JwtCredentialBuilder(IOptionsMonitor<JwtOptions> options, ILogger<JwtCredentialBuilder> logger)
    {
        _options = options;
        _logger = logger;
    }

    public Task<ICredential> Build(List<Claim> claims)
    {
        var options = _options.CurrentValue;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(options.Issuer, options.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(options.Expires), signingCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        _logger.LogDebug("build access_token：{token}", token);

        var jwtCredential = new JwtCredential
        {
            AccountId = Guid.Parse(claims.First(m => m.Type == OpenModularClaimTypes.USER_ID).Value),
            LoginTime = claims.First(m => m.Type == OpenModularClaimTypes.LOGIN_TIME).Value.ToLong(),
            AccessToken = token,
            ExpiresIn = (options.Expires < 0 ? 120 : options.Expires) * 60,
            RefreshToken = Guid.NewGuid().ToString().Replace("-", "")
        };

        return Task.FromResult<ICredential>(jwtCredential);
    }
}