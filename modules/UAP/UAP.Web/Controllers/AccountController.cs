using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Application.Accounts.Create;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Web.Models.Accounts;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("账户管理")]
public class AccountController : ModuleController
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse<AccountId>> Create(AccountCreateRequest request)
    {
        var command = Request2Command<AccountCreateCommand, AccountId>(request);
        command.Type = AccountType.Normal;

        var accountId = await Mediator.Send(command);

        return APIResponse.Success(accountId);
    }

    /// <summary>
    /// 获取单个用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse<AccountDto>> Get([BindRequired] Guid id)
    {
        var query = new AccountGetQuery(new AccountId(id));
        var account = await Mediator.Send(query);
        return APIResponse.Success(account);
    }
}