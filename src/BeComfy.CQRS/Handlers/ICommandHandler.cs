using System.Threading.Tasks;
using BeComfy.MessageBroker.RabbitMQ;

namespace BeComfy.CQRS.Handlers
{
    public interface ICommandHandler<in TCommand> 
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }         
}