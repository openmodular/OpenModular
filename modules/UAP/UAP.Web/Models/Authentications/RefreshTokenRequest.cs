using System.ComponentModel.DataAnnotations;

namespace OpenModular.Module.UAP.Web.Models.Authentications;

public class RefreshTokenRequest
{
    /// <summary>
    /// 刷新令牌
    /// </summary>
    [Required]
    public string RefreshToken { get; set; }

    /// <summary>
    /// 终端
    /// </summary>
    public string Client { get; set; }
}