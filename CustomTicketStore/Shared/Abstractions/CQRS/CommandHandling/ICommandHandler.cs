namespace CustomTicketStore.Shared.Abstractions.CQRS.CommandHandling;

using MediatR;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand;
public interface ICommandHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : ICommand<TResponse>;
