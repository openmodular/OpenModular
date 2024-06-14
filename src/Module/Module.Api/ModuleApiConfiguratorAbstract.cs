using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions.Configurator;

namespace OpenModular.Module.Api;

public abstract class ModuleApiConfiguratorAbstract : IModuleApiConfigurator
{
    public virtual void PreConfigureService(IModuleConfiguratorContext context)
    {
    }

    public virtual void ConfigureService(IModuleConfiguratorContext context)
    {
    }

    public virtual void PostConfigureService(IModuleConfiguratorContext context)
    {
    }

    public virtual void PreConfigure(IApplicationBuilder app, IHostEnvironment env)
    {
    }

    public virtual void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
    }

    public virtual void PostConfigure(IApplicationBuilder app, IHostEnvironment env)
    {
    }
}