using NodaTime;

namespace CustomTicketStore.Shared.Abstractions.CQRS;

public sealed record class CommandResponse<T>(T Reference, string ActionToken, string? State = default) where T : class
{
    public Instant Instant => SystemClock.Instance.GetCurrentInstant();
}
