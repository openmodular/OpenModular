using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OpenModular.Module.Blazor;

namespace OpenModular.Host.Blazor;

public class OpenModularBlazorHost
{
    private readonly WebAssemblyHostBuilder _builder;

    public OpenModularBlazorHost(string[] args)
    {
        _builder = WebAssemblyHostBuilder.CreateDefault(args);
        _builder.RootComponents.Add<App>("#app");
        _builder.RootComponents.Add<HeadOutlet>("head::after");

        Services = _builder.Services;
    }

    public IServiceCollection Services { get; }

    /// <summary>
    /// Register a module blazor.
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    public void RegisterModule<TModule>() where TModule : IModuleBlazor, new()
    {
        var module = new TModule();

        Services.RegisterModule(module);
    }

    /// <summary>
    /// Run ~
    /// </summary>
    public Task RunAsync()
    {
        var app = _builder.Build();
        return app.RunAsync();
    }
}