﻿using OpenModular.Common.Utils;

namespace OpenModular.Host.Web.Options;

/// <summary>
/// Open Api 选项
/// </summary>
public class OpenApiOptions
{
    public const string Position = $"{OpenModularConstants.Name}:OpenApi";

    public bool Enable { get; set; }
}