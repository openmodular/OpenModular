namespace OpenModular.Common.Utils.Models;

/// <summary>
/// 树节点结构
/// </summary>
public class TreeNodeModel<TKey, TData>
{
    public TreeNodeModel(TKey id, string label)
    {
        Id = id;
        Label = label;
    }

    /// <summary>
    /// 编号
    /// </summary>
    public TKey Id { get; set; }

    /// <summary>
    /// 文本
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 绑定的数据对象
    /// </summary>
    public TData Data { get; set; }

    /// <summary>
    /// 子节点
    /// </summary>
    public List<TreeNodeModel<TKey, TData>> Children { get; set; } = new();
}