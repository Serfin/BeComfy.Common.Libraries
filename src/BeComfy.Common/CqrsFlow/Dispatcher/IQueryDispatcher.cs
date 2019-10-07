using System.Threading.Tasks;

namespace BeComfy.Common.CqrsFlow.Dispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}