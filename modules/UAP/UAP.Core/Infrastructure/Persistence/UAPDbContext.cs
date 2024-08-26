using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence;

public class UAPDbContext(DbContextOptions<UAPDbContext> dbContextOptions) : OpenModularDbContext<UAPDbContext>(dbContextOptions, UAPConstants.ModuleCode)
{
    public DbSet<Domain.Configs.Config> Configs { get; set; }

    public DbSet<Organization> Organizations { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserDepartment> UserDepartments { get; set; }

    public DbSet<DataSeedingHistory> DataSeedingHistories { get; set; }
}