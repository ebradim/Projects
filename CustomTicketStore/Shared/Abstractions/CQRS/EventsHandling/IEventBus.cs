namespace CustomTicketStore.Shared.Abstractions.CQRS.EventsHandling;

using MediatR;
using System.Threading.Tasks;

public interface IEventBus
{
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken) where TNotification : INotification;

}
