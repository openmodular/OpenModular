using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Domain.Departments;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class UserDepartmentConfiguration : IEntityTypeConfiguration<UserDepartment>
{
    public void Configure(EntityTypeBuilder<UserDepartment> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(UserDepartment)}");

        builder.HasNoKey();

        builder.Property(x => x.UserId).IsRequired().ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new UserId(v));

        builder.Property(x => x.DepartmentId).IsRequired().ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new DepartmentId(v));
    }
}