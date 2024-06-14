using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Api;

public interface IModuleApi
{
    IModule Module { get; }

    void PreConfigureService(IModuleConfigureContext context);

    void ConfigureService(IModuleConfigureContext context);

    void PostConfigureService(IModuleConfigureContext context);

    void PreConfigure(IApplicationBuilder app, IHostEnvironment env);

    void Configure(IApplicationBuilder app, IHostEnvironment env);

    void PostConfigure(IApplicationBuilder app, IHostEnvironment env);
}