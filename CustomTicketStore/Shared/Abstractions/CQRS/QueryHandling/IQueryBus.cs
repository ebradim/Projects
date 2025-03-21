namespace CustomTicketStore.Shared.Abstractions.CQRS.QueryHandling;

using System.Threading.Tasks;

public interface IQueryBus
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
}
