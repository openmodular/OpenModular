using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Domain.Integrations;

/// <summary>
/// 集成目标
/// </summary>
public class IntegrationTarget : AggregateRoot<IntegrationTargetId>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 集成架构
    /// </summary>
    public IntegrationSchema Schema { get; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; }

    /// <summary>
    /// 状态
    /// </summary>
    public IntegrationTargetStatus Status { get; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public UserId CreatedBy { get; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedAt { get; }

    public IntegrationTarget()
    {
        //for ef
    }

    private IntegrationTarget(string name, IntegrationSchema schema, string connectionString, IntegrationTargetStatus status, UserId createdBy)
    {
        Name = name;
        Schema = schema;
        ConnectionString = connectionString;
        Status = status;
        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.Now;
    }

    /// <summary>
    /// 创建一个集成目标
    /// </summary>
    /// <param name="name"></param>
    /// <param name="schema"></param>
    /// <param name="connectionString"></param>
    /// <param name="status"></param>
    /// <param name="createdBy"></param>
    /// <returns></returns>
    public static IntegrationTarget Create(string name, IntegrationSchema schema, string connectionString, IntegrationTargetStatus status, UserId createdBy)
    {
        return new IntegrationTarget(name, schema, connectionString, status, createdBy);
    }
}