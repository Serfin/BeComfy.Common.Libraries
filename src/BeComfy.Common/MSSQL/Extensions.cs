using System.Data;
using Autofac;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace BeComfy.Common.MSSQL
{
    public static class Extensions
    {
        public static void AddSqlServer(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<ConnectionString>("sqlserver");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var connectionString = context.Resolve<ConnectionString>();

                return new SqlConnector(connectionString);
            }).InstancePerLifetimeScope();

            builder.RegisterType<SqlConnector>().As<ISqlConnector>()
                 .InstancePerDependency();
        }
    }
}