using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Common.Utils.Helpers;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Authentications;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class AuthenticationRecordConfiguration : IEntityTypeConfiguration<AuthenticationRecord>
{
    public void Configure(EntityTypeBuilder<AuthenticationRecord> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(AuthenticationRecord)}");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(50)
            .ValueGeneratedNever()
            .HasConversion(v => v.Schema, v => AuthenticationSource.GetOrCreate(v));

        builder.Property(x => x.Client)
            .IsRequired()
            .HasMaxLength(50)
            .ValueGeneratedNever()
            .HasConversion(v => v.Name, v => AuthenticationClient.GetOrCreate(v));

        builder.Property(x => x.IPv4)
            .HasConversion(v => new IpHelper().Ipv4ToInt(v), v => new IpHelper().IntToIpv4(v));

        builder.Property(x => x.IPv6)
            .HasMaxLength(100);

        builder.Property(x => x.Mac)
            .HasMaxLength(100);

        builder.Property(x => x.AccountId)
            .ValueGeneratedNever()
            .HasConversion(v => v != null ? v.Value : Guid.Empty, v => v.IsEmpty() ? null : new AccountId(v));
    }
}