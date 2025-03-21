namespace CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;

using System.Threading.Tasks;

public interface ICommandBus
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);

}
