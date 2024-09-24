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

    public JwtOptions Get()
    {
        var jwtConfig = _config.Authentication.Jwt;
        return new JwtOptions
        {
            Key = jwtConfig.Key,
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience,
            Expires = jwtConfig.Expires,
            RefreshTokenExpires = jwtConfig.RefreshTokenExpires
        };
    }
}