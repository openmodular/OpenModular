using Microsoft.AspNetCore.Mvc;

namespace OpenModular.Module.Web;

/// <summary>
/// 控制器抽象基类
/// </summary>
[ApiController]
[Route("api/[area]/[controller]/[action]")]
public abstract class ControllerAbstract : ControllerBase
{
}