using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Host.Abstractions;
using OpenModular.Module.Abstractions;
using Serilog;

namespace OpenModular.Host.Core
{
    public abstract class OpenModularHostAbstract : IOpenModularHost
    {
        public IModuleCollection Modules { get; protected set; }

        protected IServiceCollection Services;
        protected IHostBuilder _hostBuilder;

        protected OpenModularHostAbstract(string[] args)
        {
        }

        public abstract void Run();


        protected virtual void ConfigureHost()
        {
            UseSerilog();
        }
    }
}
