using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class DataSeedingHistoryConfiguration : IEntityTypeConfiguration<DataSeedingHistory>
{
    public void Configure(EntityTypeBuilder<DataSeedingHistory> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(DataSeedingHistory)}");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.Value,
            v => new DataSeedingHistoryId(v));

        builder.Property(x => x.ModuleCode).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Version).IsRequired().HasMaxLength(50);
    }
}