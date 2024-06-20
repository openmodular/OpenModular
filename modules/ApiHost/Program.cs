using OpenModular.Host.Api;
using OpenModular.Module.UAP.Api;

var host = new OpenModularApiHost(args);

host.RegisterModuleApi<UAPModuleApi>();

await host.RunAsync();