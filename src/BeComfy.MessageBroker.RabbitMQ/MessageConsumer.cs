using System;
using BeComfy.MessageBroker.RabbitMQ.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _messageBrokerConnection;
        private readonly string _appName;

        public MessageConsumer(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _messageBrokerConnection = _serviceProvider.GetService<IConnection>();
            _appName = _serviceProvider.GetService<IConfiguration>().GetValue<string>("app");
        }

        public IMessageConsumer ConsumeMessage<TMessage>() 
            where TMessage : IMessage
        {
            var queueName = nameof(TMessage).ToKebabCase();
            using (var channel = _messageBrokerConnection.CreateModel())
            {
                var queueDeclareResult = channel.QueueDeclare(queueName, false, false, false, null);
                
                channel.Close();
            }

            return this;
        }
        
        public IMessageConsumer ConsumeEvent<TEvent>() 
            where TEvent : IEvent
        {
            throw new NotImplementedException();
        }
    }
}