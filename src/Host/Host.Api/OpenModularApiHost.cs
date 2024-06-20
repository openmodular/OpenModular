using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Host.Abstractions;
using OpenModular.Host.Api.Middlewares;
using OpenModular.Module.Api;
using OpenModular.Module.Core;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;
using Serilog;

namespace OpenModular.Host.Api;

public class OpenModularApiHost : IOpenModularHost
{
    private readonly WebApplicationBuilder _builder;
    private readonly IServiceCollection _services;

    public OpenModularApiHost(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        UseSerilog();

        _services = _builder.Services;

        _services.AddModuleApiService();
    }

    /// <summary>
    /// 注册模块API
    /// </summary>
    /// <typeparam name="TModuleApi"></typeparam>
    public void RegisterModuleApi<TModuleApi>() where TModuleApi : IModuleApi, new()
    {
        var moduleApi = new TModuleApi();

        _services.AddModuleApi(moduleApi);
    }

    /// <summary>
    /// 启动~
    /// </summary>
    public async Task RunAsync()
    {
        var moduleConfigureContext = new ModuleConfigureContext(_services, _builder.Environment, _builder.Configuration);

        _services.AddModuleApiPreConfigureService(moduleConfigureContext);

        _services.AddOpenModularOpenApi();

        _services.AddOpenModularMediatR();

        _services.AddOpenModularCors();

        _services.AddPersistence(_builder.Configuration);

        _services.AddDataSeedingCore();

        _services.AddModuleApiConfigureService(moduleConfigureContext);

        _services.AddModuleApiPostConfigureService(moduleConfigureContext);

        _services.AddOpenModularMiddlewares();

        var app = _builder.Build();

        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<DbMigrationHandler>().MigrateAsync();

        app.UseOpenModularEndpoints();

        app.UseMiddleware<ExceptionHandleMiddleware>();

        app.UseOpenApi();

        app.UseMiddleware<UnitOfWorkMiddleware>();

        await app.RunAsync();
    }

    /// <summary>
    /// 使用Serilog日志
    /// </summary>
    private void UseSerilog()
    {
        _builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom
                .Configuration(hostingContext.Configuration)
                .Enrich
                .FromLogContext();
        });
    }

}
