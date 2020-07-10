using System.Threading.Tasks;

namespace BeComfy.CQRS.Dispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}