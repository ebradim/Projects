namespace CustomTicketStore.Shared.Infrastructures.CQRS;

using MediatR;
using Microsoft.Extensions.Logging;
using CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;
using System.Threading.Tasks;

internal sealed class CommandBus(IMediator mediator, ILogger<CommandBus> logger) : ICommandBus
{
    public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending command: {command}", command);
        return await mediator.Send<TResponse>(command, cancellationToken);
    }

    async Task ICommandBus.Send<TCommand>(TCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending command: {command}", command);
        await mediator.Send(command, cancellationToken);
    }
}
