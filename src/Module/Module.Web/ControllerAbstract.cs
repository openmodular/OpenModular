using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Authorization;

namespace OpenModular.Module.Web;

[Route("api/[area]/[controller]/[action]")]
[Authorize(Policy = OpenModularAuthorizationRequirement.Name)]
public abstract class ControllerAbstract : ControllerBaseAbstract;