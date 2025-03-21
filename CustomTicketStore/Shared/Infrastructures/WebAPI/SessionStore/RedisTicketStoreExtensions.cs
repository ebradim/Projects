using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace CustomTicketStore.Shared.Infrastructures.WebAPI.SessionStore;

public static class RedisTicketStoreExtensions
{
    public static void AddRedisTicketStore(this IServiceCollection services)
    {
        services.AddSingleton<ITicketStore, SessionTicketStore>();

    }
}
