using System.ComponentModel.DataAnnotations;
using OpenModular.Authentication.Abstractions;

namespace OpenModular.Module.UAP.Web.Models.Authentications;

public class LoginRequest
{
    /// <summary>
    /// 认证模式
    /// </summary>
    [Required]
    public AuthenticationMode Mode { get; set; }

    /// <summary>
    /// 认证源
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// 身份载体
    /// </summary>
    public required string Payload { get; set; }

    /// <summary>
    /// 终端
    /// </summary>
    public required string Client { get; set; }
}