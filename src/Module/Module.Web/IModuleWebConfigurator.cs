using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Web;

public interface IModuleWebConfigurator
{
    void PreConfigureService(IModuleConfiguratorContext context);

    void ConfigureService(IModuleConfiguratorContext context);

    void PostConfigureService(IModuleConfiguratorContext context);

    void ConfigureMvc(IMvcBuilder mvcBuilder);

    void PreConfigure(IApplicationBuilder app, IHostEnvironment env);

    void Configure(IApplicationBuilder app, IHostEnvironment env);

    void PostConfigure(IApplicationBuilder app, IHostEnvironment env);
}