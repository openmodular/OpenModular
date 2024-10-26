using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Domain.Authentications;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence;

public class UAPDbContext(DbContextOptions<UAPDbContext> dbContextOptions) : EfDbContext<UAPDbContext>(UAPConstants.ModuleCode, dbContextOptions)
{
    public DbSet<Config> Configs { get; set; }

    //public DbSet<Organization> Organizations { get; set; }

    //public DbSet<Department> Departments { get; set; }

    public DbSet<Account> Accounts { get; set; }

    //public DbSet<AccountDepartment> UserDepartments { get; set; }

    public DbSet<DataSeedingHistory> DataSeedingHistories { get; set; }

    public DbSet<AuthenticationRecord> AuthenticationRecords { get; set; }

    public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
}