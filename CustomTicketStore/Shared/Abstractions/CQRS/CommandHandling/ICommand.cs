namespace CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;

using MediatR;
using CustomTicketStore.Shared.Abstractions.CQRS;

public interface ICommand : IRequest
{
}
public interface ICommand<TResponse> : IRequest<TResponse>
{
}