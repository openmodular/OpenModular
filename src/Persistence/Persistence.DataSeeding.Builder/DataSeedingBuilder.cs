using System.Reflection;
using LiteDB;

namespace OpenModular.Persistence.DataSeeding.Builder;

/// <summary>
/// 种子数据库构造器
/// </summary>
public class DataSeedingBuilder
{
    private readonly string _dbFileDir;
    private readonly List<Assembly> _modules = new();

    public DataSeedingBuilder(string dbFileDir)
    {
        if (string.IsNullOrEmpty(dbFileDir))
            throw new ArgumentNullException(nameof(dbFileDir));

        if (!Directory.Exists(dbFileDir))
            Directory.CreateDirectory(dbFileDir);

        _dbFileDir = dbFileDir;
    }

    public void Register<T>() where T : class
    {
        _modules.Add(typeof(T).Assembly);
    }

    public void Build()
    {
        Build(DataSeedingConstants.DbFileName, DataSeedingConstants.DbPassword);
    }

    /// <summary>
    /// 生成预置数据库文件
    /// </summary>
    /// <param name="dbFileName">数据库文件名称</param>
    public void Build(string dbFileName)
    {
        Build(dbFileName, DataSeedingConstants.DbPassword);
    }

    /// <summary>
    /// 生成预置数据库文件
    /// </summary>
    /// <param name="dbFileName">数据库文件名称</param>
    /// <param name="dbPassword">数据库密码</param>
    public void Build(string dbFileName, string dbPassword)
    {
        using var db = new LiteDatabase(new ConnectionString
        {
            Filename = Path.Combine(_dbFileDir, dbFileName),
            Password = dbPassword
        });

        //删除表
        db.DropCollection(nameof(DataSeedingRecord));

        var col = db.GetCollection<DataSeedingRecord>();

        foreach (var assembly in _modules)
        {
            var migrationDefinitionTypes = assembly.GetTypes().Where(m => !m.IsInterface && !m.IsAbstract && typeof(IDataSeedingDefinition).IsAssignableFrom(m)).ToList();

            foreach (var definitionType in migrationDefinitionTypes)
            {
                //获取数据集
                var instance = (IDataSeedingDefinition)Activator.CreateInstance(definitionType);

                var data = (List<DataSeedingRecord>)definitionType.GetMethod("Get")!.Invoke(instance, null);

                if (data != null && data.Any())
                {
                    col.Insert(data);
                }
            }
        }

        Console.WriteLine("导入完成");
    }
}