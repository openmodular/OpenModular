using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Common.Utils;
using OpenModular.Host.Abstractions;
using OpenModular.Host.Web.Middlewares;
using OpenModular.Host.Web.OpenApi;
using OpenModular.Host.Web.Options;
using OpenModular.Module.Core;
using OpenModular.Module.Web;
using OpenModular.Persistence;
using Serilog;

namespace OpenModular.Host.Web;

public class OpenModularWebHost : IOpenModularHost
{
    private readonly WebApplicationBuilder _builder;
    private readonly IServiceCollection _services;
    private readonly WebHostOptions _hostOptions;

    public OpenModularWebHost(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        _hostOptions = new WebHostOptions();

        LoadOptions();

        UseSerilog();

        _builder.WebHost.UseUrls(_hostOptions.Urls);

        _services = _builder.Services;

        _services.AddModuleWebService();
    }

    /// <summary>
    /// 注册模块API
    /// </summary>
    /// <typeparam name="TModuleWeb"></typeparam>
    public void RegisterModuleWeb<TModuleWeb>() where TModuleWeb : IModuleWeb, new()
    {
        var moduleApi = new TModuleWeb();

        _services.RegisterModuleWeb(moduleApi);
    }

    /// <summary>
    /// 启动~
    /// </summary>
    public Task RunAsync()
    {
        ConfigureService();

        return ConfigureAsync();
    }

    private void ConfigureService()
    {
        var moduleConfigureContext = new ModuleConfigureContext(_services, _builder.Environment, _builder.Configuration);

        _services.AddCommonUtils();

        _services.AddOutputCache();

        _services.AddModuleWebPreConfigureService(moduleConfigureContext);

        _services.AddOpenModularMvc();

        _services.AddOpenModularOpenApi();

        _services.AddOpenModularMediatR();

        _services.AddOpenModularCors(_hostOptions);

        _services.AddPersistence(_builder.Configuration);

        _services.AddModuleWebConfigureService(moduleConfigureContext);

        _services.AddModuleWebPostConfigureService(moduleConfigureContext);

        _services.AddOpenModularMiddlewares();
    }

    private async Task ConfigureAsync()
    {
        var app = _builder.Build();

        app.ExecuteDbMigration();

        app.ExecuteDataSeeding();

        app.UseMiddleware<ExceptionHandleMiddleware>();

        app.UseOutputCache();

        app.UseOpenModularOpenApi();

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

    /// <summary>
    /// 加载宿主配置项
    /// </summary>
    /// <returns></returns>
    private void LoadOptions()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", false);

        var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environmentVariable.NotNull())
        {
            configBuilder.AddJsonFile($"appsettings.{environmentVariable}.json", false);
        }

        var config = configBuilder.Build();
        config.GetSection(WebHostOptions.Position).Bind(_hostOptions);

        if (_hostOptions.Urls.IsNull())
            _hostOptions.Urls = "http://*:6220";
    }
}
