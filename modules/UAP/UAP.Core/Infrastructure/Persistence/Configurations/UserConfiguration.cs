using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_User");

        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.Status);
        builder.Property(x => x.ActivatedTime);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new UserId(v));

    }
}