using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Configs;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class ConfigConfiguration : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(Config)}");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedNever().HasConversion(
            v => v.Value,
            v => new ConfigId(v));

        builder.Property(x => x.ModuleCode).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Key).IsRequired().HasMaxLength(300);
    }
}