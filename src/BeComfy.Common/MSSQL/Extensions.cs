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

            builder.RegisterType<SqlConnector>().As<ISqlConnector>();
        }

        public static ISqlConnector UseSqlServer(this IApplicationBuilder app)
            => new SqlConnector(app);
    }
}