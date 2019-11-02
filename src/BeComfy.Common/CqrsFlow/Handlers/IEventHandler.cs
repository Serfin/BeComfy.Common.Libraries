using System.Threading.Tasks;
using BeComfy.Common.RabbitMq;

namespace BeComfy.Common.CqrsFlow.Handlers
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}