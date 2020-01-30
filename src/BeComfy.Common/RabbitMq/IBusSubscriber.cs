using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Types.Exceptions;

namespace BeComfy.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, BeComfyException, IRejectedEvent> onError = null)
                where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,            
            Func<TEvent, BeComfyException, IRejectedEvent> onError = null)
                where TEvent : IEvent;
    }
}