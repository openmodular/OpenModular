using System.ComponentModel.DataAnnotations;

namespace OpenModular.Module.UAP.Web.Models.Auths;

public class RefreshTokenRequest
{
    /// <summary>
    /// 刷新令牌
    /// </summary>
    [Required]
    public string RefreshToken { get; set; }
}