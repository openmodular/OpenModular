using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Web;

internal interface IModuleWebConfigurator
{
    void PreConfigureService(IModuleConfiguratorContext context);

    void ConfigureService(IModuleConfiguratorContext context);

    void PostConfigureService(IModuleConfiguratorContext context);

    void PreConfigure(IApplicationBuilder app, IHostEnvironment env);

    void Configure(IApplicationBuilder app, IHostEnvironment env);

    void PostConfigure(IApplicationBuilder app, IHostEnvironment env);
}