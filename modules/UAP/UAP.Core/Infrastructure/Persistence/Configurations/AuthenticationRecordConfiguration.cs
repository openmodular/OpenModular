using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils.Helpers;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Authentications;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class AuthenticationRecordConfiguration : IEntityTypeConfiguration<AuthenticationRecord>
{
    public void Configure(EntityTypeBuilder<AuthenticationRecord> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(AuthenticationRecord)}");

        builder.Property(x => x.Source).IsRequired().ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => AuthenticationSource.GetOrCreate(v));

        builder.Property(x => x.Terminal).IsRequired().ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => AuthenticationTerminal.GetOrCreate(v));

        builder.Property(x => x.IPv4).HasConversion(
            v => new IpHelper().Ipv4ToInt(v),
            v => new IpHelper().IntToIpv4(v));

        builder.Property(x => x.UserId).ValueGeneratedNever().HasConversion(
            v => v != null ? v.ToString() : "",
            v => v.IsNull() ? null : new UserId(v));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}