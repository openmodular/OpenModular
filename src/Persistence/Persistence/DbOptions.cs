namespace OpenModular.Persistence;

/// <summary>
/// 数据库配置项
/// </summary>
public class DbOptions
{
    public const string Position = "OpenModular:Db";

    /// <summary>
    /// 数据库提供器
    /// </summary>
    public DbProvider Provider { get; set; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public required string ConnectionString { get; set; }
}