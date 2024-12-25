using OpenModular.Host.Blazor;
using OpenModular.Module.UAP.Blazor;

namespace OpenModular.Module.BlazorHost;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new OpenModularBlazorHost(args);

        host.RegisterModule<UAPModuleBlazor>();

        await host.RunAsync();
    }
}