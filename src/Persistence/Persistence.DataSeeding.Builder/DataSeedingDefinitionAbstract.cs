using System.Reflection;
using System.Text.Json;
using OpenModular.Configuration.Abstractions;

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
        DataCollection.Add(new DataSeedingRecord(Module, typeof(TEntity).FullName!, DataSeedingMode.Insert, JsonSerializer.Serialize(entity), _version));
    }

    /// <summary>
    /// 添加更新记录
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected void AddUpdate<TEntity>(TEntity entity) where TEntity : class
    {
        DataCollection.Add(new DataSeedingRecord(Module, typeof(TEntity).FullName!, DataSeedingMode.Update, JsonSerializer.Serialize(entity), _version));
    }

    /// <summary>
    /// 添加删除记录
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    protected void AddDelete<TEntity>(string id) where TEntity : class
    {
        DataCollection.Add(new DataSeedingRecord(Module, typeof(TEntity).FullName!, DataSeedingMode.Delete, id, _version));
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

    /// <summary>
    /// 解析配置类
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    /// <param name="config"></param>
    /// <returns></returns>
    protected Dictionary<string, string> ResolveConfig<TConfig>(TConfig config) where TConfig : IConfig
    {
        var dic = new Dictionary<string, string>();
        Object2Dictionary(config, dic, String.Empty);
        return dic;
    }

    private void Object2Dictionary(object obj, Dictionary<string, string> configDictionary, string parentKey)
    {
        var objType = obj.GetType();
        foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var key = parentKey.IsNull() ? property.Name : $"{parentKey}:{property.Name}";
            var value = property.GetValue(obj);

            if (value != null)
            {
                if (IsSimpleType(property.PropertyType))
                {

                    configDictionary[key] = value.ToString() ?? string.Empty;
                }
                else
                {
                    Object2Dictionary(value, configDictionary, key);
                }
            }
        }
    }

    private bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal);
    }
}