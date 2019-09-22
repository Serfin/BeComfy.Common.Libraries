using System.Threading.Tasks;
using BeComfy.Common.Messages;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;

namespace BeComfy.Common.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }
        public async Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) 
            where TCommand : ICommand
        {
            await _busClient.PublishAsync(command, ctx => ctx.UseMessageContext(context));    
        }
    }
}