using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.Authentications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Authentication.Abstractions;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class AuthenticationTokenConfiguration : IEntityTypeConfiguration<AuthenticationToken>
{
    public void Configure(EntityTypeBuilder<AuthenticationToken> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(AuthenticationToken)}");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.Value,
            v => new AccountId(v));

        builder.Property(x => x.AccessToken).IsRequired();
        builder.Property(x => x.RefreshToken).IsRequired().HasMaxLength(100);

        builder.Property(x => x.Client).IsRequired().ValueGeneratedNever().HasConversion(
            v => v != null ? v.Name : string.Empty,
            v => AuthenticationClient.Find(v));
    }
}