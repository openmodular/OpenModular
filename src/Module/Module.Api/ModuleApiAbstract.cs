using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Api;

/// <summary>
/// 定义模块API的抽象类
/// </summary>
/// <typeparam name="TModule"></typeparam>
public abstract class ModuleApiAbstract<TModule> : IModuleApi where TModule : IModule, new()
{
    public IModule Module => Activator.CreateInstance<TModule>();

    public virtual void PreConfigureService(IModuleConfigureContext context)
    {
    }

    public virtual void ConfigureService(IModuleConfigureContext context)
    {
    }

    public virtual void PostConfigureService(IModuleConfigureContext context)
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