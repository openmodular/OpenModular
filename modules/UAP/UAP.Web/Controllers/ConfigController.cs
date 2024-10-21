using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.DDD.Core.Application.Dto;
using OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;
using OpenModular.Module.UAP.Web.Models.Configs;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("配置管理")]
public class ConfigController : ModuleController
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<APIResponse<PagedDto<ConfigDto>>> PagingQuery([FromQuery] ConfigPagingQueryRequest request)
    {
        var query = ObjectMapper.Map<ConfigPagedQuery>(request);

        var result = await Mediator.Send(query);

        return APIResponse.Success(result);
    }
}