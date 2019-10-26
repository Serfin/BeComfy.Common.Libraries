
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeComfy.Common.EFCore
{
    public static class Extensions
    {
        private static readonly string SectionName = "sqlserver";
        public static void AddEFCoreContext<T>(this IServiceCollection services)
            where T : DbContext
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var options = configuration.GetOptions<SqlServerSettings>(SectionName);

            services.AddDbContext<T>(dbContextBuilder => {
                dbContextBuilder.UseSqlServer(options.PrepareConnectionString(), 
                    cmdTimeout => cmdTimeout.CommandTimeout(options.CommandTimeout));
            });
        }

        private static string PrepareConnectionString(this SqlServerSettings connectionString)
            => string.Concat(
                "Server=", connectionString.Host, ";", 
                "Database=" + connectionString.Database, ";",
                "User Id=", connectionString.UserId, ";",
                "Password=" + connectionString.Password + ";");
    }
}