using Microsoft.Extensions.DependencyInjection;

namespace OpenModular.Host.Web.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddOpenModularMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandleMiddleware>();
        services.AddScoped<UnitOfWorkMiddleware>();

        return services;
    }

}