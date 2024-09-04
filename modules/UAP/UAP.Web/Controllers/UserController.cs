using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Application.Users.Create;
using OpenModular.Module.UAP.Core.Application.Users.Get;
using OpenModular.Module.UAP.Web.Models.Users;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("用户管理")]
public class UserController : ModuleController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
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
    public async Task<APIResponse<UserId>> Create(UserCreateRequest request)
    {
        var command = _mapper.Map<UserCreateCommand>(request);
        command.CreatedBy = UserId;

        var userId = await _mediator.Send(command);

        return APIResponse.Success(userId);
    }

    /// <summary>
    /// 获取单个用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse<UserDto>> Get([BindRequired] Guid id)
    {
        var query = new UserGetQuery(new UserId(id));

        var userId = await _mediator.Send(query);
        return APIResponse.Success(userId);
    }
}