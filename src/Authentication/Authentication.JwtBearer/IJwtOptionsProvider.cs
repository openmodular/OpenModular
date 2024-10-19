namespace OpenModular.Authentication.JwtBearer;

/// <summary>
/// Jwt配置项提供器，用于动态获取配置信息
/// </summary>
public interface IJwtOptionsProvider
{
    ValueTask<JwtOptions> GetAsync();
}