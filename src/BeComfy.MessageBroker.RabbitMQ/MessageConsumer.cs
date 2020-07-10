using System;
using System.Text;
using BeComfy.MessageBroker.RabbitMQ.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _messageBrokerConnection;
        private readonly IModel _channel;
        private readonly ILogger<IMessageConsumer> _logger;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly App _app = new App();

        private readonly bool _loggerEnabled;

        public MessageConsumer(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _messageBrokerConnection = _serviceProvider.GetService<IConnection>();
            _channel = _messageBrokerConnection.CreateModel();
            _logger = _serviceProvider.GetService<ILogger<IMessageConsumer>>();
            _rabbitMqOptions = _serviceProvider.GetService<RabbitMqOptions>();

            _loggerEnabled = _rabbitMqOptions.LoggerEnabled ?? true;

            var configuration = _serviceProvider.GetService<IConfiguration>();
            configuration.GetSection("app").Bind(_app);
        }

        public IMessageConsumer ConsumeMessage<TMessage>() 
            where TMessage : IMessage
        {
            var queueName = string.Concat(_app.Name, "/", typeof(TMessage).Name.ToKebabCase());
            var routingKey = MessageBrokerUtilities.GetRoutingKey<TMessage>();

            if (_loggerEnabled)
            {
                _logger.LogInformation($"Declaring a queue - [{queueName}]");             
            }

            var queueDeclareResult = _channel.QueueDeclare(queueName, _rabbitMqOptions.QueueOptions.Durable,
                _rabbitMqOptions.QueueOptions.Exclusive, _rabbitMqOptions.QueueOptions.AutoDelete, 
                _rabbitMqOptions.QueueOptions.Arguments);

            if (_loggerEnabled)
            {
                _logger.LogInformation($"Binding [{queueName}] to [{_rabbitMqOptions.ExchangeOptions.Name}]" +
                    $" with routing key [{routingKey}]");
            }

            _channel.QueueBind(queueName, _rabbitMqOptions.ExchangeOptions.Name, 
                routingKey, null);

            var consumerModel = new EventingBasicConsumer(_channel);

            consumerModel.Received += (sender, eventArgs) => 
            {
                if (_loggerEnabled)
                {
                    var timestamp = eventArgs.BasicProperties.Timestamp.UnixTime;
                    _logger.LogInformation($"Received message from [{queueName}] - [{timestamp}]");                  
                }
            };

            _channel.BasicConsume(queueName, true, consumerModel);

            return this;
        }
        
        public IMessageConsumer ConsumeEvent<TEvent>() 
            where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void TryHandleMessage<TMessage>()
        {
            var commandHandler = _serviceProvider.GetService<ICommandHandler<TMessage>>();
        }
    }
}