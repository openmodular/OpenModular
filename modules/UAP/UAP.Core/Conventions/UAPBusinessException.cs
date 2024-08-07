﻿using OpenModular.Module.Abstractions.Exceptions;

namespace OpenModular.Module.UAP.Core.Conventions;

public class UAPBusinessException(UAPErrorCode errorCode, string message = null) : ModuleBusinessException(UAPConstants.ModuleCode, errorCode, message);