using OpenModular.Common.Utils.Paging;

namespace OpenModular.Module.UAP.Web.Models.Configs;

public record ConfigPagingQueryRequest
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public Pagination Pagination { get; set; } = new();

    /// <summary>
    /// 模块编码
    /// </summary>
    public string ModuleCode { get; set; }
}