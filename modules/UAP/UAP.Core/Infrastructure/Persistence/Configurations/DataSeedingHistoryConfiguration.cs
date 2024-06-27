using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class DataSeedingHistoryConfiguration : IEntityTypeConfiguration<DataSeedingHistory>
{
    public void Configure(EntityTypeBuilder<DataSeedingHistory> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(DataSeedingHistory)}");

        builder.Property(x => x.ModuleCode).IsRequired();
        builder.Property(x => x.Version).IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new DataSeedingHistoryId(v));

    }
}