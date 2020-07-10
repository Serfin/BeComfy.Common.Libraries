// using System.Threading.Tasks;
// using Autofac;
// using BeComfy.Common.CqrsFlow.Handlers;
// using Microsoft.CSharp;
// using System.Dynamic;

// namespace BeComfy.CQRS.Dispatcher
// {
//     public class QueryDispatcher : IQueryDispatcher
//     {
//         private readonly IComponentContext _context;

//         public QueryDispatcher(IComponentContext context)
//         {
//             _context = context;
//         }

//         public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
//         {
//             var handlerType = typeof(IQueryHandler<,>)
//                 .MakeGenericType(query.GetType(), typeof(TResult));

//             dynamic handler = _context.Resolve(handlerType);

//             return await handler.HandleAsync((dynamic)query);
//         }
//     }
// }