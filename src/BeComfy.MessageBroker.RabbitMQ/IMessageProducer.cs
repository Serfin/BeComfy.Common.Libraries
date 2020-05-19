using System.Threading.Tasks;
using BeComfy.MessageBroker.RabbitMQ.Messages;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public interface IMessageProducer
    {
        Task SendMessageAsync<TMessage>(TMessage message)
            where TMessage : IMessage;

        Task SendEventAsync<TEvent>(TEvent message)
            where TEvent : IEvent;
    }
}