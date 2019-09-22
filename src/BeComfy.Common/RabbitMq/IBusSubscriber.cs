using System.Threading.Tasks;
using BeComfy.Common.Messages;

namespace BeComfy.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        Task SubscribeCommand<TCommand>(string @namespace = null, string queueName = null)
            where TCommand : ICommand;
    }
}