using System.Threading.Tasks;
using BeComfy.Common.Messages;

namespace BeComfy.Common.CqrsFlow.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}