using OpenModular.Module.Abstractions.Exceptions;

namespace OpenModular.Module.UAP.Core;

internal class UAPBusinessException(UAPErrorCode errorCode, string message = null) : ModuleBusinessException(UAPConstants.ModuleCode, errorCode, message)
{

}