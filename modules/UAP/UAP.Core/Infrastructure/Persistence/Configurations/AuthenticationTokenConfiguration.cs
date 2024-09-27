using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Authentications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class AuthenticationTokenConfiguration : IEntityTypeConfiguration<AuthenticationToken>
{
    public void Configure(EntityTypeBuilder<AuthenticationToken> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(AuthenticationToken)}");

        builder.Property(x => x.AccessToken).IsRequired();
        builder.Property(x => x.RefreshToken).IsRequired();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new UserId(v));
    }
}