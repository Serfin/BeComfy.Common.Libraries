using System;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace BeComfy.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _retries;
        private readonly int _retryInterval;
        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
            var options = _serviceProvider.GetService<RabbitMqOptions>();
            _retries = options.Retries >= 0 ? options.Retries : 3;
            _retryInterval = options.RetryInterval > 0 ? options.RetryInterval : 2;
        }

        public async Task SubscribeCommand<TCommand>(string @namespace = null, string queueName = null)
            where TCommand : ICommand
        {
            await _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                await commandHandler.HandleAsync(command, correlationContext);
            });
        }
    }
}