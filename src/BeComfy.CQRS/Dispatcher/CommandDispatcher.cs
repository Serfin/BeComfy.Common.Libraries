// using System.Threading.Tasks;
// using Autofac;
// using BeComfy.CQRS.Handlers;
// using BeComfy.Common.Messages;
// using BeComfy.Common.RabbitMq;

// namespace BeComfy.CQRS.Dispatcher
// {
//     public class CommandDispatcher : ICommandDispatcher
//     {
//         private readonly IComponentContext _context;

//         public CommandDispatcher(IComponentContext context)
//         {
//             _context = context;
//         }
        
//         public async Task SendAsync<T>(T command) where T : ICommand
//             => await _context.Resolve<ICommandHandler<T>>()
//                 .HandleAsync(command, CorrelationContext.Empty);
//     }
// }