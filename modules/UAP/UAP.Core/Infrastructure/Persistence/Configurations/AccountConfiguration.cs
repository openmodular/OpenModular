﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(Account)}");

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.NormalizedUserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(300);
        builder.Property(x => x.NormalizedEmail).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Tel).HasMaxLength(50);
        builder.Property(x => x.RealName).HasMaxLength(50);
        builder.Property(x => x.NickName).HasMaxLength(50);
        builder.Property(x => x.Avatar).HasMaxLength(300);
        builder.Property(x => x.Status);
        builder.Property(x => x.ActivatedTime);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever().HasConversion(
            v => v.ToString(),
            v => new AccountId(v));

        builder.Property(x => x.CreatedBy).ValueGeneratedNever().HasConversion(
            v => v != null ? v.ToString() : string.Empty,
            v => v.IsNullOrWhiteSpace() ? null : new AccountId(v));

        builder.Property(x => x.TenantId).ValueGeneratedNever().HasConversion(
            v => v != null ? v.ToString() : string.Empty,
            v => v.IsNullOrWhiteSpace() ? null : new TenantId(v));

    }
}