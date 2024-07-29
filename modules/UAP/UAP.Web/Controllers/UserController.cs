using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Module.UAP.Core.Application.Users.CreateUser;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Web.Models.Users;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("用户管理")]
public class UserController : ModuleController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse<UserId>> Create(UserCreateRequest request)
    {
        var command = request.Adapt<CreateUserCommand>();
        var userId = await _mediator.Send(command);
        return APIResponse.Success(userId);
    }
}