using OpenModular.Host.Web;
using OpenModular.Module.UAP.Web;

var host = new OpenModularWebHost(args);

host.RegisterModuleWeb<UAPModuleWeb>();

await host.RunAsync();