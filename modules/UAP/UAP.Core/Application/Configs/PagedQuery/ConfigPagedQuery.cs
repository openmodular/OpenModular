using OpenModular.DDD.Core.Application.Query;

namespace OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;

public class ConfigPagedQuery : PagedQueryBase<ConfigDto>
{
    public string ModuleCode { get; set; }
}