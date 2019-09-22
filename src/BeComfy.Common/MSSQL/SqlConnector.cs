using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BeComfy.Common.MSSQL
{
    public class SqlConnector : ISqlConnector
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _connectionString;

        public SqlConnector(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _connectionString = PrepareConnectionString(_serviceProvider.GetService<ConnectionString>());
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private string PrepareConnectionString(ConnectionString connectionString)
            => string.Concat(connectionString.Host, connectionString.Port, ";", 
                connectionString.Database, ";", connectionString.TrustedConnection);
    }
}