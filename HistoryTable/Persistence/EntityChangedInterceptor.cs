using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;



namespace HistoryTable.Persistence;
internal sealed class EntityChangedInterceptor() : SaveChangesInterceptor
{

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));
        ArgumentNullException.ThrowIfNull(eventData.Context, nameof(eventData.Context));

        var auditableEntities = eventData.Context.ChangeTracker.Entries()
                .Where(e => !e.Metadata.ClrType.Equals(typeof(Archive)))
                .Where(e => e.State is EntityState.Modified or EntityState.Deleted);

        var archives = Enumerable.Empty<Archive>();

        using var enumerator = auditableEntities.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var entry = enumerator.Current;
            if (entry.State is EntityState.Deleted)
                archives = archives.Append(new Archive()
                {
                    Entity = JsonDocument.Parse(JsonSerializer.Serialize(entry.Entity)),
                    TableName = entry.Metadata.GetTableName()!,
                    Event = ArchiveEvent.EntityRemoved,
                    UserId = 1
                });

            //user must be authenticated to dispatch modifying event
            if (entry.State is EntityState.Modified)
                archives = archives.Append(new Archive()
                {
                    Entity = JsonDocument.Parse(JsonSerializer.Serialize(entry.OriginalValues.ToObject())),
                    TableName = entry.Metadata.GetTableName()!,
                    Event = ArchiveEvent.EntityModified,
                    UserId = 1
                });
        }
        if (archives.Any())
        {
            eventData.Context.AddRange(archives);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}

