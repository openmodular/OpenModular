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

        builder.Property(x => x.Client).IsRequired().ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => AuthenticationClient.GetOrCreate(v));

        builder.Property(x => x.IPv4).HasConversion(
            v => new IpHelper().Ipv4ToInt(v),
            v => new IpHelper().IntToIpv4(v));

        builder.Property(x => x.AccountId).ValueGeneratedNever().HasConversion(
            v => v != null ? v.ToString() : "",
            v => v.IsNull() ? null : new AccountId(v));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}