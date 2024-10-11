using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(Organization)}");

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Code).IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new OrganizationId(v));

        builder.Property(x => x.CreatedBy).ValueGeneratedNever().HasConversion(
            v => v != null ? v.ToString() : string.Empty,
            v => v.IsNull() ? null : new AccountId(v));
    }
}