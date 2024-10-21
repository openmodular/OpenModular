//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;
//using OpenModular.DDD.Core.Domain.Entities.TypeIds;
//using OpenModular.Module.UAP.Core.Domain.Accounts;
//using OpenModular.Module.UAP.Core.Domain.Departments;

//namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Configurations;

//public class AccountDepartmentConfiguration : IEntityTypeConfiguration<AccountDepartment>
//{
//    public void Configure(EntityTypeBuilder<AccountDepartment> builder)
//    {
//        builder.ToTable($"{UAPConstants.ModuleCode}_{nameof(AccountDepartment)}");

//        builder.HasNoKey();

//        builder.Property(x => x.AccountId).IsRequired().ValueGeneratedNever().HasConversion(
//            v => v.Value,
//            v => new AccountId(v));

//        builder.Property(x => x.DepartmentId).IsRequired().ValueGeneratedNever().HasConversion(
//            v => v.Value,
//            v => new DepartmentId(v));
//    }
//}