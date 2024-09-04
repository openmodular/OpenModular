namespace OpenModular.Authentication.JwtBearer;

public interface IJwtOptionsProvider
{
    Task<JwtOptions> GetAsync();
}