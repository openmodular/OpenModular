using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Host.Abstractions;
using OpenModular.Host.Api.Middlewares;
using OpenModular.Module.Abstractions.Configurator;
using OpenModular.Module.Api;
using OpenModular.Module.Core;
using OpenModular.Persistence;
using Serilog;

namespace OpenModular.Host.Api;

public class OpenModularApiHost : IOpenModularHost
{
    private readonly ModuleApiCollection _moduleApiCollection = [];
    private readonly WebApplicationBuilder _builder;
    public IServiceCollection Services { get; }

    public OpenModularApiHost(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        UseSerilog();

        Services = _builder.Services;

        Services.AddSingleton<IModuleApiCollection>(_moduleApiCollection);

        Services.AddModuleCoreService();
    }

    /// <summary>
    /// 注册模块API
    /// </summary>
    /// <typeparam name="TModuleApi"></typeparam>
    public void RegisterModuleApi<TModuleApi>() where TModuleApi : IModuleApi, new()
    {
        var moduleApi = new TModuleApi();
        var module = moduleApi.Module;

        var coreAssembly = module.GetType().Assembly;
        var apiAssembly = moduleApi.GetType().Assembly;

        var moduleConfiguratorType = coreAssembly.GetTypes().FirstOrDefault(m =>
            typeof(IModuleConfigurator).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract);

        if (moduleConfiguratorType != null)
        {
            var configurator = (IModuleConfigurator)Activator.CreateInstance(moduleConfiguratorType);
        }


        Services.AddModule(moduleApi.Module);
    }

    /// <summary>
    /// 启动~
    /// </summary>
    public async Task RunAsync()
    {
        var moduleConfigureContext = new ModuleConfigureContext(Services, _builder.Environment, _builder.Configuration, Modules);

        Services.AddModulePreService(moduleConfigureContext);

        Services.AddModuleApiPreConfigureService(moduleConfigureContext, _moduleApiCollection);

        Services.AddOpenModularOpenApi(_moduleApiCollection);

        Services.AddMediatR(_moduleApiCollection);

        Services.AddCors();

        Services.AddPersistence(_builder.Configuration);

        Services.AddModuleService(moduleConfigureContext, Modules);

        Services.AddModuleApiConfigureService(moduleConfigureContext, _moduleApiCollection);

        Services.AddModulePostService(moduleConfigureContext, Modules);

        Services.AddModuleApiPostConfigureService(moduleConfigureContext, _moduleApiCollection);

        Services.AddScoped<UnitOfWorkMiddleware>();

        var app = _builder.Build();

        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<DbMigrationHandler>().MigrateAsync();

        app.UseOpenModularEndpoints(_moduleApiCollection);

        app.UseOpenApi(_moduleApiCollection);

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
