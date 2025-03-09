using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HistoryTable.Persistence;

public sealed class Archive
{
    public int Id { get; set; }
    public required JsonDocument Entity { get; set; }
    public required string TableName { get; set; }
    public required ArchiveEvent Event { get; set; }
    public int? UserId { get; set; }
    public DateTime DateCreated { get; init; }
}
