using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class ConfigConfiguration : IEntityTypeConfiguration<Domain.Configs.Config>
{
    public void Configure(EntityTypeBuilder<Domain.Configs.Config> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(Config)}");

        builder.Property(x => x.ModuleCode).IsRequired();
        builder.Property(x => x.Key).IsRequired();

        builder.HasKey(x => x.Id);
    }
}