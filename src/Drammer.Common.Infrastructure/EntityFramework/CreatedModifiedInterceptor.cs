using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Drammer.Common.Infrastructure.EntityFramework;

public sealed class CreatedModifiedInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
        {
            UpdateEntities(eventData.Context);
        }

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext context)
    {
        var utcNow = DateTime.UtcNow;
        var entities = context.ChangeTracker.Entries<ICreatedModifiedEntity>().ToList();

        foreach (var entry in entities)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    SetCurrentPropertyValue(
                        entry, nameof(ICreatedModifiedEntity.Created), utcNow);
                    SetCurrentPropertyValue(
                        entry, nameof(ICreatedModifiedEntity.Modified), utcNow);
                    break;
                case EntityState.Modified:
                    SetCurrentPropertyValue(
                        entry, nameof(ICreatedModifiedEntity.Modified), utcNow);
                    ExcludeProperty(entry, nameof(ICreatedModifiedEntity.Created));
                    break;
            }
        }

        return;

        static void SetCurrentPropertyValue(
            EntityEntry entry,
            string propertyName,
            DateTime utcNow) =>
            entry.Property(propertyName).CurrentValue = utcNow;

        static void ExcludeProperty(
            EntityEntry entry,
            string propertyName) =>
            entry.Property(propertyName).IsModified = false;
    }
}