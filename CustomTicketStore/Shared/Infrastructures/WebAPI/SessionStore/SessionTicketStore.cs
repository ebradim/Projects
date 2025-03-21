namespace CustomTicketStore.Shared.Infrastructures.WebAPI.SessionStore;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

public sealed class SessionTicketStore(IDistributedCache cache) : ITicketStore
{
    public static string GenerateKey(string authScheme, string userName)
    {
        return $"{KeyPrefix}-{authScheme}-{userName}";

    }
    private const string KeyPrefix = nameof(SessionTicketStore);

    public async Task RemoveAsync(string key)
    {
        await cache.RemoveAsync(key);
    }

    public async Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        var options = new DistributedCacheEntryOptions();
        var expiresUtc = ticket.Properties.ExpiresUtc;
        if (expiresUtc.HasValue)
        {
            options.SetAbsoluteExpiration(expiresUtc.Value);
        }
        var val = SerializeToBytes(ticket);
        await cache.SetAsync(key, val, options);
    }

    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        var bytes = await cache.GetAsync(key);
        return DeserializeFromBytes(bytes);
    }

    public async Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = GenerateKey(ticket.AuthenticationScheme, ticket.Principal.Identity?.Name!);
        await RenewAsync(key, ticket);
        return key;
    }
    private static byte[] SerializeToBytes(AuthenticationTicket source)
    {
        return TicketSerializer.Default.Serialize(source);
    }

    private static AuthenticationTicket? DeserializeFromBytes(byte[]? source)
    {
        return source is not null ? TicketSerializer.Default.Deserialize(source) : default;
    }
}
