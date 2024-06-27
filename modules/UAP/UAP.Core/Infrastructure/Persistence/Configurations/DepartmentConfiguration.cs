using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Departments;
using OpenModular.Module.UAP.Core.Domain.Organizations;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(Department)}");

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.ParentId).IsRequired();
        builder.Property(x => x.Code).IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new DepartmentId(v));

        builder.Property(x => x.ParentId).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new DepartmentId(v));

        builder.Property(x => x.OrganizationId).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new OrganizationId(v));

        builder.Property(x => x.CreatedBy).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new UserId(v));
    }
}