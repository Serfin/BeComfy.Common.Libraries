using BeComfy.MessageBroker.RabbitMQ.Messages;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public interface IMessageConsumer
    {
        IMessageConsumer ConsumeMessage<TMessage>()
            where TMessage : IMessage;

        IMessageConsumer ConsumeEvent<TEvent>()
            where TEvent : IEvent;
    }
}