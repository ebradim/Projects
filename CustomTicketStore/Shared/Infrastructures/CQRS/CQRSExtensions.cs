namespace CustomTicketStore.Shared.Infrastructures.CQRS;
using Microsoft.Extensions.DependencyInjection;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using CustomTicketStore.Shared.Abstractions.CQRS.EventsHandling;
using CustomTicketStore.Shared.Abstractions.CQRS.QueryHandling;
using System;
using System.Linq;

public static class CQRSExtensions
{
    // private const string MODULE_PREFIX = "CustomTicketStore.Modules";

    public static void AddCQRSModules(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<Program>();
            // var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            //     .Where(e => e.FullName is not null && e.FullName.StartsWith(MODULE_PREFIX, StringComparison.OrdinalIgnoreCase));
            // config.RegisterServicesFromAssemblies([.. assemblies]);
            //config.AddOpenRequestPostProcessor(typeof(CachingPostProcessor<,>));
            //config.AddOpenBehavior(typeof(EmailConfirmingPipeline<,>));
            //config.AddOpenBehavior(typeof(CachingPipelineBehaviour<,>));
        })
        .AddScoped<ICommandBus, CommandBus>();
    }
}
