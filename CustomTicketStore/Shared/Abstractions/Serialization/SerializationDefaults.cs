using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomTicketStore.Shared.Abstractions.Serialization;

public static class SerializationDefaults
{
    public static JsonSerializerOptions CachingOpts => new()
    {
        PropertyNameCaseInsensitive = true
    };
}
