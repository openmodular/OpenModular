using Microsoft.AspNetCore.Authorization;

namespace OpenModular.Authorization;

public class OpenModularAuthorizationRequirement : IAuthorizationRequirement
{
    public const string Name = "OpenModular";
}