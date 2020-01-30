using System;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.CqrsFlow;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Microsoft.Extensions.Logging;
using RawRabbit.Common;
using BeComfy.Common.Types.Exceptions;

namespace BeComfy.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IBusPublisher _busPublisher;
        private readonly IServiceProvider _serviceProvider;

        public BusSubscriber(IApplicationBuilder app)
        {
            _logger = app.ApplicationServices.GetService<ILogger<BusSubscriber>>();
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
            _busPublisher = _serviceProvider.GetService<IBusPublisher>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, BeComfyException, IRejectedEvent> onError = null)
                where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                return await TryHandleAsync(command, correlationContext,
                    () => commandHandler.HandleAsync(command, correlationContext), onError);
            });

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, BeComfyException, IRejectedEvent> onError = null)
                where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent, CorrelationContext>(async (@event, correlationContext) =>
            {
                var eventHandler = _serviceProvider.GetService<IEventHandler<TEvent>>();

                return await TryHandleAsync(@event, correlationContext,
                    () => eventHandler.HandleAsync(@event, correlationContext), onError);
            });

            return this;
        }
        
        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message,
            CorrelationContext correlationContext, Func<Task> handle,
            Func<TMessage, BeComfyException, IRejectedEvent> onError = null)
        {          
            var messageName = message.GetType().Name;

            try
            {
                _logger.LogInformation($"Handling a message: '{messageName}' " +
                        $"with correlation id: '{correlationContext.Id}'.");

                await handle();

                _logger.LogInformation($"Successfully handled a message: '{messageName}' " +
                        $"with correlation id: '{correlationContext.Id}'.");

                return new Ack();
            }
            catch (Exception ex)
            {
                if (ex is BeComfyException beComfyException && onError != null)
                {
                    var rejectedEvent = onError(message, beComfyException);
                    await _busPublisher.PublishAsync(rejectedEvent, correlationContext);
                
                    _logger.LogInformation($"Unable to handle a message: '{messageName}' " +
                        $"with correlation id: '{correlationContext.Id}'. " + 
                        $"Rejected event '{rejectedEvent.GetType().Name}' was published!");

                    return new Ack();
                }

                _logger.LogInformation($"Unable to handle a message: '{messageName}' " +
                    $"with correlation id: '{correlationContext.Id}'. Rejected event not defined!");

                throw new BeComfyException("rejected_event_not_defined", "Cannot process message, rejected event not defined");
            }   
        }
    }
}