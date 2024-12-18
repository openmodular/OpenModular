﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using OpenModular.DDD.Core.Uow;
using Microsoft.Data.Sqlite;
using OpenModular.Persistence.DataSeeding.Json;
using OpenModular.Persistence.Uow;

namespace OpenModular.Persistence.DataSeeding.Internal;

internal class DefaultDataSeedingHandler<TDbContext> : IDataSeedingHandler where TDbContext : EfDbContext<TDbContext>
{
    private readonly DataSeedingOptions _options;
    private readonly IDataSeedingHistoryStorage _historyStorage;
    private readonly ILogger<DefaultDataSeedingHandler<TDbContext>> _logger;
    private readonly IEnumerable<IDataSeedingFilter> _filters;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _moduleCode;

    public DefaultDataSeedingHandler(TDbContext dbContext, IDataSeedingHistoryStorage historyStorage, IOptions<DataSeedingOptions> options, ILogger<DefaultDataSeedingHandler<TDbContext>> logger, IEnumerable<IDataSeedingFilter> filters, IServiceProvider serviceProvider)
    {
        _options = options.Value;
        _historyStorage = historyStorage;
        _logger = logger;
        _filters = filters;
        _serviceProvider = serviceProvider;

        _moduleCode = dbContext.ModuleCode;

        dbContext.Dispose();
    }

    public async Task DoAsync()
    {
        Check.NullOrWhiteSpace(_options.DbFileName, nameof(_options.DbFileName));

        var lastVersion = await _historyStorage.GetLastVersionAsync(_moduleCode);

        _logger.LogDebug("Default data seeding handler[{module}] start,the last version is {version}", _moduleCode, lastVersion);

        var seedingRecords = await GetRecordAsync(lastVersion);

        if (!seedingRecords.Any())
        {
            _logger.LogDebug("Default data seeding handler[{module}] not found  seeding record,handle finished.", _moduleCode);

            return;
        }

        _logger.LogDebug("Default data seeding handler[{module}] found [{count}] seeding record", _moduleCode, seedingRecords.Count);

        var groups = seedingRecords.GroupBy(m => m.Version);
        foreach (var group in groups)
        {
            using var scope = _serviceProvider.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>() as UnitOfWork;
            var historyStorage = scope.ServiceProvider.GetRequiredService<IDataSeedingHistoryStorage>();
            var dbContext = await uow!.GetDbContextAsync<TDbContext>();

            foreach (var record in group)
            {
                _logger.LogInformation("Handle data migration record start,the record is [{@record}]", record);

                try
                {
                    if (_filters != null && _filters.Any())
                    {
                        var isContinue = false;
                        foreach (var filter in _filters)
                        {
                            if (!await filter.Before(record))
                            {
                                isContinue = true;
                                break;
                            }
                        }

                        if (isContinue)
                        {
                            continue;
                        }
                    }

                    switch (record.Mode)
                    {
                        case DataSeedingMode.Insert:
                            HandleInsert(record, dbContext);
                            break;
                        case DataSeedingMode.Update:
                            HandleUpdate(record, dbContext);
                            break;
                        case DataSeedingMode.Delete:
                            HandleDelete(record, dbContext);
                            break;
                        case DataSeedingMode.SQL:
                            HandleSql(record, dbContext);
                            break;
                    }

                    if (_filters != null && _filters.Any())
                    {
                        foreach (var filter in _filters)
                        {
                            await filter.After(record);
                        }
                    }

                    _logger.LogInformation("Handle data migration record finished");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Data migration error :" + ex);
                    throw;
                }
            }

            await historyStorage.InsertVersionAsync(_moduleCode, group.Key);

            await dbContext.SaveChangesAsync();

            await uow.CompleteAsync();
        }
    }

    private async Task<List<DataSeedingRecord>> GetRecordAsync(int lastVersion)
    {
        var connectionString = new SqliteConnectionStringBuilder($"Data Source={_options.DbFileName}")
        {
            Mode = SqliteOpenMode.ReadWriteCreate
        }.ToString();

        await using var con = new SqliteConnection(connectionString);
        con.Open();

        // 设置加密密钥
        var command = con.CreateCommand();

        command.CommandText = $"SELECT * FROM DataSeedingRecord WHERE Module='{_moduleCode}' AND Version>{lastVersion}";
        await using var reader = await command.ExecuteReaderAsync();
        var seedingRecords = new List<DataSeedingRecord>();

        while (await reader.ReadAsync())
        {
            var record = new DataSeedingRecord(
                reader.GetString(reader.GetOrdinal(nameof(DataSeedingRecord.Module))),
                reader.GetString(reader.GetOrdinal(nameof(DataSeedingRecord.EntityName))),
                (DataSeedingMode)reader.GetInt32(reader.GetOrdinal(nameof(DataSeedingRecord.Mode))),
                reader.GetString(reader.GetOrdinal(nameof(DataSeedingRecord.Data))),
                reader.GetInt32(reader.GetOrdinal(nameof(DataSeedingRecord.Version))),
                (DataSeedingSqlMode)reader.GetInt32(reader.GetOrdinal(nameof(DataSeedingRecord.SqlMode)))
            );

            seedingRecords.Add(record);
        }

        return seedingRecords;
    }

    private void HandleInsert(DataSeedingRecord record, TDbContext dbContext)
    {
        var entityType = dbContext.Model.FindEntityType(record.EntityName)!.ClrType;
        var dbSet = dbContext.GetType().GetMethod("Set", [])!.MakeGenericMethod(entityType).Invoke(dbContext, []);
        var dbSetType = dbSet!.GetType();

        var options = new JsonSerializerOptions
        {
            Converters = { new PrivateSettersContractResolver() }
        };

        var entity = JsonSerializer.Deserialize(record.Data, entityType, options);

        dbSetType.GetMethod("Add", [entityType])!.Invoke(dbSet, new[] { entity });
    }

    private void HandleUpdate(DataSeedingRecord record, TDbContext dbContext)
    {
        var dbContextType = dbContext.GetType();
        var entityType = dbContext.Model.FindEntityType(record.EntityName)!.ClrType;
        var dbSet = dbContextType.GetMethod("Set", [])!.MakeGenericMethod(entityType)
            .Invoke(dbContext, []);
        var dbSetType = dbSet!.GetType();

        var entity = JsonSerializer.Deserialize(record.Data, entityType);
        if (entity == null)
        {
            _logger.LogError("Data seeding update handle error,the entity[{entity}] is null", record.EntityName);
            return;
        }

        dbSetType.GetMethod("Attach", [entityType])!.Invoke(dbSet, [entity]);
        var entry = dbContext.Entry(entity);
        entry.State = EntityState.Modified;
    }

    private void HandleDelete(DataSeedingRecord record, TDbContext dbContext)
    {
        var dbContextType = dbContext.GetType();
        var entityType = dbContext.Model.FindEntityType(record.EntityName)!.ClrType;
        var dbSet = dbContextType.GetMethod("Set", [])!.MakeGenericMethod(entityType)
            .Invoke(dbContext, []);

        var findMethod = dbSet!.GetType().GetMethod("Find", BindingFlags.Public | BindingFlags.Instance);
        var entity = findMethod!.Invoke(dbSet, [new object[] { record.Data }]);
        if (entity == null)
        {
            _logger.LogError("Data seeding update handle error,the entity[{entity}][{id}] not found", record.EntityName, record.Data);
            return;
        }

        var removeMethod = dbSet.GetType().GetMethod("Remove", BindingFlags.Public | BindingFlags.Instance);
        removeMethod!.Invoke(dbSet, [entity]);
    }

    private void HandleSql(DataSeedingRecord record, TDbContext dbContext)
    {
        if (string.IsNullOrWhiteSpace(record.Data))
            return;

        var dbProvider = dbContext.GetDatabaseProvider();
        if (record.SqlMode == DataSeedingSqlMode.Common || dbProvider.ToString().EqualsIgnoreCase(record.SqlMode.ToString()))
        {
            dbContext.Database.ExecuteSqlRaw(record.Data);
        }
    }
}