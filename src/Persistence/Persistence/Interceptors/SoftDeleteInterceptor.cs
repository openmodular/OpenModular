using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils;
using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Persistence.Interceptors;

/// <summary>
/// 软删除拦截器
/// </summary>
internal class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<ISoftDelete>> entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<ISoftDelete>()
                .Where(e => e.State == EntityState.Deleted);

        foreach (EntityEntry<ISoftDelete> softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Entity.IsDeleted = true;
            softDeletable.Entity.DeletedAt = DateTimeOffset.UtcNow;

            var currentAccount = GlobalServiceProvider.GetService<ICurrentAccount>();
            if (currentAccount != null)
            {
                softDeletable.Entity.DeletedBy = currentAccount.AccountId;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}