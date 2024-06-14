using Microsoft.Extensions.DependencyInjection;

namespace OpenModular.Host.Abstractions;

public interface IOpenModularHost
{
    IServiceCollection Services { get; }
}