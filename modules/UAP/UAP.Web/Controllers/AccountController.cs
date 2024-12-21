using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.DDD.Core.Domain.Exceptions;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("账户")]
public class AccountController : ModuleController
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<APIResponse<AccountDto>> Get([FromQuery] AccountId id)
    {
        var query = new GetAccountQuery { Id = id };
        var dto = await Mediator.Send(query);
        if (dto == null)
            throw new EntityNotFoundException();

        return APIResponse.Success(dto);
    }
}