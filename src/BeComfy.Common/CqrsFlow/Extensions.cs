using System.Reflection;
using Autofac;
using BeComfy.Common.CqrsFlow.Dispatcher;
using BeComfy.Common.CqrsFlow.Handlers;

namespace BeComfy.Common.CqrsFlow
{
    public static class Extensions
    {
        public static void AddDispatcher(this ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
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