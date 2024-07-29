using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Web;

public abstract class ModuleWebConfiguratorAbstract : IModuleWebConfigurator
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

    public void ConfigureMvc(IMvcBuilder mvcBuilder)
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