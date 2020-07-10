using System.Threading.Tasks;
using BeComfy.MessageBroker.RabbitMQ;

namespace BeComfy.CQRS.Handlers
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}