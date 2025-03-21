namespace CustomTicketStore.Shared.Abstractions.CQRS.QueryHandling;

using MediatR;
using CustomTicketStore.Shared.Abstractions.CQRS;


public interface IQuery<TResponse> : IRequest<TResponse>
{

}