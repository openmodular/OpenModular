using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Module.UAP.Core;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web;

[Area(UAPConstants.ModuleCode)]
public abstract class ModuleController : ControllerAbstract
{
    protected ModuleController(IMapper objectMapper, IMediator mediator) : base(objectMapper, mediator)
    {
    }
}