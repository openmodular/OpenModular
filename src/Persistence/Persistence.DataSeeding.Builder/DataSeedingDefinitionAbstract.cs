using System.Text.Json;

namespace OpenModular.Persistence.DataSeeding.Builder;

public abstract class DataSeedingDefinitionAbstract : IDataSeedingDefinition
{
    protected List<DataSeedingRecord> DataCollection = new();
    private readonly int _version;

    public abstract string Module { get; }

    protected DataSeedingDefinitionAbstract()
    {
        _version = Convert.ToInt32(GetType().Name.Split("_")[1]);
    }

    public IList<DataSeedingRecord> Get()
    {
        Define();

        return DataCollection;
    }

    public abstract void Define();

    /// <summary>
    /// 添加插入记录
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected void AddInsert<TEntity>(TEntity entity) where TEntity : class
    {
        DataCollection.Add(new DataSeedingRecord(Module, typeof(TEntity).Name, DataSeedingMode.Insert, JsonSerializer.Serialize(entity), _version));
    }

    /// <summary>
    /// 添加更新记录
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected void AddUpdate<TEntity>(TEntity entity) where TEntity : class
    {
        DataCollection.Add(new DataSeedingRecord(Module, typeof(TEntity).Name, DataSeedingMode.Update, JsonSerializer.Serialize(entity), _version));
    }

    /// <summary>
    /// 添加删除记录
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    protected void AddDelete<TEntity>(string id) where TEntity : class
    {
        DataCollection.Add(new DataSeedingRecord(Module, typeof(TEntity).Name, DataSeedingMode.Delete, id, _version));
    }

    /// <summary>
    /// 添加SQL记录
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected void AddSQL(string sql)
    {
        DataCollection.Add(new DataSeedingRecord(Module, string.Empty, DataSeedingMode.SQL, sql, _version));
    }

    /// <summary>
    /// 添加SQLite记录
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected void AddSqlite(string sql)
    {
        DataCollection.Add(new DataSeedingRecord(Module, string.Empty, DataSeedingMode.SQL, sql, _version, DataSeedingSqlMode.Sqlite));
    }

    /// <summary>
    /// 添加PostgreSQL记录
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected void AddPostgreSQL(string sql)
    {
        DataCollection.Add(new DataSeedingRecord(Module, string.Empty, DataSeedingMode.SQL, sql, _version, DataSeedingSqlMode.PostgreSql));
    }

    /// <summary>
    /// 添加MySql记录
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected void AddMySql(string sql)
    {
        DataCollection.Add(new DataSeedingRecord(Module, string.Empty, DataSeedingMode.SQL, sql, _version, DataSeedingSqlMode.MySql));
    }

    /// <summary>
    /// 添加MySql记录
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected void AddSqlServer(string sql)
    {
        DataCollection.Add(new DataSeedingRecord(Module, string.Empty, DataSeedingMode.SQL, sql, _version, DataSeedingSqlMode.SqlServer));
    }
}