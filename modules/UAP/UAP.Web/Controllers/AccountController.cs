using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Application.Accounts.Create;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;
using OpenModular.Module.UAP.Web.Models.Accounts;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("账户管理")]
public class AccountController : ModuleController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AccountController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse<AccountId>> Create(AccountCreateRequest request)
    {
        var command = _mapper.Map<AccountCreateCommand>(request);
        command.CreatedBy = CurrentAccountId;

        var accountId = await _mediator.Send(command);

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
        var query = new AccountGetQuery
        {
            AccountId = new AccountId(id)
        };

        var account = await _mediator.Send(query);
        return APIResponse.Success(account);
    }
}