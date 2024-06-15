using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Api;

public interface IModuleApi
{
    IModule Module { get; }
}