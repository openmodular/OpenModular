using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OpenModular.Module.UAP.Core;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

namespace OpenModular.Module.UAP.Migrations.Sqlite;

public class UAPDbContextFactory : IDesignTimeDbContextFactory<UAPDbContext>
{
    public UAPDbContext CreateDbContext(string[] args)
    {
        //dotnet ef migrations add uap_v0001

        var builder = new DbContextOptionsBuilder<UAPDbContext>()
            .UseSqlite("data source=./Data/Database/OpenModular.sdb", o => o.MigrationsAssembly(typeof(UAPDbContextFactory).Assembly.FullName));
   
        return new UAPDbContext(builder.Options, new UAPModule());
    }
}