

using Microsoft.Extensions.DependencyInjection;
using CustomTicketStore.Shared.Abstractions.Constants;
using StackExchange.Redis;

namespace CustomTicketStore.Shared.Infrastructures.WebAPI;

public static class RedisExtensions
{
    /// <summary>
    /// Quick add Redis to the service collection, this quick setup is suitable for <c>development</c> environment.<para>
    /// For <c>Production</c> environment, use <see cref="AddRedisNodes"/>
    ///</para>
    /// </summary>
    public static void AddRedis(this IServiceCollection services, string hostAndPort)
    {
        services.AddStackExchangeRedisCache(config =>
        {
            config.InstanceName = $"{ApplicationDefaults.NAME}:";
            config.Configuration = hostAndPort;
        });
    }
    /// <summary>
    /// Add Redis to the service collection, this setup is suitable for <c>Production</c> environment.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="password">The password of cluster used in connection</param>
    /// <param name="nodes">Collection of nodes within cluster</param>
    public static void AddRedisNodes(this IServiceCollection services, string password, IList<string> nodes)
    {

        services.AddStackExchangeRedisCache(config =>
        {
            config.InstanceName = $"{ApplicationDefaults.NAME}:";
            config.ConfigurationOptions = new()
            {
                CommandMap = CommandMap.Create(
                [
                    "INFO", "CONFIG", "CLUSTER", "PING", "ECHO", "CLIENT"
                ], available: false), // disable these commands for client

                AllowAdmin = true,
                AbortOnConnectFail = false,
                ConnectRetry = 5,
                SyncTimeout = 5000,
                AsyncTimeout = 5000,
                Password = password,
            };
            for (int i = 0; i < nodes.Count; i++)
            {
                config.ConfigurationOptions.EndPoints.Add(nodes[i]);
            }
        });
    }
}
