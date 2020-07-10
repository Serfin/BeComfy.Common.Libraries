using System.Threading.Tasks;

namespace BeComfy.CQRS.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}