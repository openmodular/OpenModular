using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenModular.Authentication.JwtBearer;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加JwtBearer服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtBearer(this IServiceCollection services)
    {
        //添加凭证构造器
        services.AddTransient<JwtSecurityTokenBuilder>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                };

                //先清除再添加自定义令牌验证器
                options.TokenHandlers.Clear();
                options.TokenHandlers.Add(new CustomJwtSecurityTokenHandler());
            });

        return services;
    }
}