using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow;

namespace BeComfy.Common.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand;
    }
}