using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Cache.Core;
using OpenModular.Common.Utils;
using OpenModular.Host.Abstractions;
using OpenModular.Host.Web.Middlewares;
using OpenModular.Host.Web.OpenApi;
using OpenModular.Host.Web.Options;
using OpenModular.Module.Abstractions;
using OpenModular.Module.Core;
using OpenModular.Module.Web;
using Serilog;

namespace OpenModular.Host.Web;

public class OpenModularWebHost : IOpenModularHost
{
    private readonly WebApplicationBuilder _builder;
    private readonly WebHostOptions _hostOptions;
    private readonly OpenApiOptions _openApiOptions;

    public IServiceCollection Services { get; }

    public OpenModularWebHost(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

        _hostOptions = new WebHostOptions();
        _openApiOptions = new OpenApiOptions();

        LoadOptions();

        UseSerilog();

        _builder.WebHost.UseUrls(_hostOptions.Urls);

        Services = _builder.Services;

        Services.AddModuleWebService();
    }

    /// <summary>
    /// 注册模块
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    public void RegisterModule<TModule>() where TModule : IModule, new()
    {
        var module = new TModule();

        Services.RegisterModule(module);
    }

    /// <summary>
    /// 使用模块的Web端注册模块
    /// </summary>
    /// <typeparam name="TModuleWeb"></typeparam>
    public void RegisterModuleWeb<TModuleWeb>() where TModuleWeb : IModuleWeb, new()
    {
        var moduleApi = new TModuleWeb();

        Services.RegisterModuleWeb(moduleApi);
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
        var moduleConfigureContext = new ModuleConfigureContext(Services, _builder.Environment, _builder.Configuration);

        //添加多语言支持
        Services.AddOpenModularLocalization(_builder.Configuration);

        //添加公共服务
        Services.AddCommonUtils();

        //添加领域驱动服务
        Services.AddDDD();

        //添加输出缓存服务
        Services.AddOutputCache();

        //添加模块前置服务
        Services.AddModuleWebPreConfigureService(moduleConfigureContext);

        //添加MVC服务
        Services.AddOpenModularMvc();

        //添加OpenAPI服务
        Services.AddOpenModularOpenApi(_openApiOptions);

        //添加MediatR服务
        Services.AddOpenModularMediatR();

        //添加CORS服务
        Services.AddOpenModularCors(_hostOptions);

        //解决Multipart body length limit 134217728 exceeded
        Services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = int.MaxValue;
        });

        //添加HttpClient相关
        Services.AddHttpClient();

        //添加JWT认证
        Services.AddJwtBearer();

        //添加授权服务
        Services.AddOpenModularAuthorization();

        //添加数据持久化服务
        Services.AddPersistence(_builder.Configuration);

        //添加缓存服务
        Services.AddOpenModularCache(_builder.Configuration);

        //添加配置服务
        Services.AddOpenModularConfig();

        Services.AddDatabaseDeveloperPageExceptionFilter();

        //添加模块服务
        Services.AddModuleWebConfigureService(moduleConfigureContext);

        //添加模块后置服务
        Services.AddModuleWebPostConfigureService(moduleConfigureContext);

        //添加中间件服务
        Services.AddOpenModularMiddlewares();
    }

    private async Task ConfigureAsync()
    {
        var app = _builder.Build();

        //执行数据库迁移
        if (!_hostOptions.DisableDbMigration)
            app.ExecuteDbMigration();

        //执行数据种子
        if (!_hostOptions.DisableDataSeeding)
            app.ExecuteDataSeeding();

        //异常处理
        app.UseMiddleware<ExceptionHandleMiddleware>();

        //基地址
        app.UsePathBase(_hostOptions);

        //设置默认页
        app.UseDefaultDir(_hostOptions);

        //设置默认页
        app.UseDefaultPage(_hostOptions);

        //代理
        app.UseProxy(_hostOptions);

        //HTTP跳转HTTPS
        app.UseHttpsRedirection();

        //路由
        app.UseRouting();

        //CORS
        app.UseCors("Default");

        //多语言
        app.UseRequestLocalization();

        //认证
        app.UseAuthentication();

        //授权
        app.UseAuthorization();

        //OpenAPI
        app.UseOpenModularOpenApi(_openApiOptions);

        //工作单元
        app.UseMiddleware<UnitOfWorkMiddleware>();

        //输出缓存
        app.UseOutputCache();

        //配置端点
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //启用Banner图
        app.UseBanner(app.Lifetime, _hostOptions);

        GlobalServiceProvider.Set(app.Services);

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
                .Configuration(hostingContext.Configuration.GetSection(OpenModularConstants.Name))
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
        if (environmentVariable!.NotNull())
        {
            configBuilder.AddJsonFile($"appsettings.{environmentVariable}.json", false);
        }

        var config = configBuilder.Build();
        config.GetSection(WebHostOptions.Position).Bind(_hostOptions);

        if (_hostOptions.Urls.IsNull())
            _hostOptions.Urls = "http://*:6220";

        config.GetSection(OpenApiOptions.Position).Bind(_openApiOptions);
    }
}
