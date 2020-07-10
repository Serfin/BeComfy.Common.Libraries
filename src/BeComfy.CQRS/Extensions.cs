using System.Reflection;
using Autofac;
using BeComfy.CQRS.Dispatcher;
using BeComfy.CQRS.Handlers;

namespace BeComfy.CQRS
{
    public static class Extensions
    {
        public static void AddDispatcher(this ContainerBuilder builder)
        {
            //builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            //builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
        }

        public static void AddHandlers(this ContainerBuilder builder)
        {
            var assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerDependency();
        }
    }
}