using Microsoft.AspNetCore.Mvc;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web;

[Area(UAPConstants.ModuleCode)]
public abstract class ModuleController : ControllerAbstract;