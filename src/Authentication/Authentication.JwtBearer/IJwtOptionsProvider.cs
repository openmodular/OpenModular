namespace OpenModular.Authentication.JwtBearer;

public interface IJwtOptionsProvider
{
    JwtOptions Get();
}