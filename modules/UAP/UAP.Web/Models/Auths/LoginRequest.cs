using OpenModular.Authentication.Abstractions;

namespace OpenModular.Module.UAP.Web.Models.Auths;

public class LoginRequest
{
    /// <summary>
    /// 认证模式
    /// </summary>
    public AuthenticationMode Mode { get; set; }

    /// <summary>
    /// 认证源
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// 身份载体
    /// </summary>
    public string Payload { get; set; }

    /// <summary>
    /// 终端
    /// </summary>
    public string Terminal { get; set; }
}