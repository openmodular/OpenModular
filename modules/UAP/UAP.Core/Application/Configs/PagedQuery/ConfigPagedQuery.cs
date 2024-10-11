using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Application.Query;

namespace OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;

public class ConfigPagedQuery : PageQueryBase<PagedDto<ConfigDto>>
{
    public string ModuleCode { get; set; }
}