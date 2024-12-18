﻿using System.Text.RegularExpressions;
using OpenModular.Common.Utils.DependencyInjection;

namespace OpenModular.Common.Utils.Helpers;

/// <summary>
/// IP Helper
/// </summary>
public class IpHelper : ISingletonDependency
{
    /// <summary>
    /// 判断是否是ipv4格式
    /// </summary>
    /// <param name="str">ip地址</param>
    /// <returns>如果是ipv4地址，则为true，否则为false</returns>
    public bool IsIpv4(string? str)
    {
        if (str.IsNullOrWhiteSpace())
            return false;

        return Regex.IsMatch(str, RegexExpressionConstants.IPv4);
    }

    /// <summary>
    /// 将IPv4转换为整数
    /// </summary>
    /// <param name="ip">IPv4字符串</param>
    /// <returns>代表IPv4的整数</returns>
    public uint Ipv4ToInt(string? ip)
    {
        if (!IsIpv4(ip)) return 0;

        var ipParts = ip.Split('.');
        uint intIp = 0;
        for (int i = 0; i < 4; i++)
        {
            intIp = intIp << 8;
            intIp += uint.Parse(ipParts[i]);
        }

        return intIp;
    }

    /// <summary>
    /// 将整数转换为IPv4
    /// </summary>
    /// <param name="intIp">代表IPv4的整数</param>
    /// <returns>IPv4字符串</returns>
    public string IntToIpv4(uint intIp)
    {
        if (intIp == 0) return string.Empty;

        var ipParts = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            ipParts[3 - i] = (byte)(intIp & 255);
            intIp = intIp >> 8;
        }

        return string.Join('.', ipParts);
    }
}