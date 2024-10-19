using OpenModular.Authentication.JwtBearer;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Infrastructure;

internal class DefaultJwtOptionsProvider : IJwtOptionsProvider, ITransientDependency
{
    private readonly UAPConfig _config;

    public DefaultJwtOptionsProvider(UAPConfig config)
    {
        _config = config;
    }

    public ValueTask<JwtOptions> GetAsync()
    {
        var jwtConfig = _config.Authentication.Jwt;
        if (jwtConfig == null)
        {
            throw new ArgumentException("The uap authentication jwt setting is null");
        }

        var options = new JwtOptions
        {
            Key = jwtConfig.Key,
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience,
            Expires = jwtConfig.Expires,
            RefreshTokenExpires = jwtConfig.RefreshTokenExpires
        };

        return ValueTask.FromResult(options);
    }
}