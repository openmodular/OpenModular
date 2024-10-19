using Microsoft.Data.Sqlite;
using System.Reflection;

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
        var connectionString = new SqliteConnectionStringBuilder($"Data Source={Path.Combine(_dbFileDir, dbFileName)}")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            //Password = dbPassword
        }.ToString();

        using var con = new SqliteConnection(connectionString);
        con.Open();

        var command = con.CreateCommand();

        // 删除 DataSeedingRecord 表
        command.CommandText = "DROP TABLE IF EXISTS DataSeedingRecord;";
        command.ExecuteNonQuery();

        // 创建 DataSeedingRecord 表
        command.CommandText = @"
        CREATE TABLE DataSeedingRecord (
            Module TEXT NOT NULL,
            EntityName TEXT NOT NULL,
            Mode INTEGER NOT NULL,
            Data TEXT NOT NULL,
            Version INTEGER NOT NULL,
            SqlMode INTEGER NOT NULL
        );";

        command.ExecuteNonQuery();

        foreach (var assembly in _modules)
        {
            var migrationDefinitionTypes = assembly.GetTypes().Where(m => !m.IsInterface && !m.IsAbstract && typeof(IDataSeedingDefinition).IsAssignableFrom(m)).ToList();

            foreach (var definitionType in migrationDefinitionTypes)
            {
                //获取数据集
                var instance = (IDataSeedingDefinition)Activator.CreateInstance(definitionType)!;

                var data = (List<DataSeedingRecord>)definitionType.GetMethod("Get")!.Invoke(instance, null)!;

                if (data.Any())
                {
                    foreach (var record in data)
                    {
                        command.CommandText = @"
                        INSERT INTO DataSeedingRecord (Module, EntityName, Mode, Data, Version, SqlMode)
                        VALUES (@Module, @EntityName, @Mode, @Data, @Version, @SqlMode);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Module", record.Module);
                        command.Parameters.AddWithValue("@EntityName", record.EntityName);
                        command.Parameters.AddWithValue("@Mode", record.Mode);
                        command.Parameters.AddWithValue("@Data", record.Data);
                        command.Parameters.AddWithValue("@Version", record.Version);
                        command.Parameters.AddWithValue("@SqlMode", record.SqlMode);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        Console.WriteLine("导入完成");
    }
}