using System.Threading.Tasks;

namespace BeComfy.Common.CqrsFlow.Handlers
{
    public interface IQueryHandler<TQuery, TQueryResult> 
        where TQuery : IQuery
    {
        Task<TQueryResult> HandleAsync(TQuery query);
    }
}