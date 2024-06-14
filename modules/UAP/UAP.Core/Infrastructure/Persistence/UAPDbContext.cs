using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence;

public class UAPDbContext(DbContextOptions<UAPDbContext> dbContextOptions) : OpenModularDbContext<UAPDbContext>(dbContextOptions)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<UserId>();

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}