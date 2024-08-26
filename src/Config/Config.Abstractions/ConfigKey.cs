﻿using System.Reflection;

namespace OpenModular.Config.Abstractions;

/// <summary>
/// 配置键，对应配置类的属性
/// </summary>
public class ConfigKey
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 属性
    /// </summary>
    public PropertyInfo Property { get; set; }
}