using System.Threading.Tasks;
using BeComfy.Common.RabbitMq;

namespace BeComfy.Common.CqrsFlow.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }         
}