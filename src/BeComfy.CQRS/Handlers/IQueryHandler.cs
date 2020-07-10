using System.Threading.Tasks;

namespace BeComfy.CQRS.Handlers
{
    public interface IQueryHandler<TQuery, TQueryResult> 
        where TQuery : IQuery
    {
        Task<TQueryResult> HandleAsync(TQuery query);
    }
}