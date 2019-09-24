using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BeComfy.Common.MSSQL
{
    public class SqlConnector : ISqlConnector
    {
        private readonly string _connectionString;

        public SqlConnector(ConnectionString connectionString)
        {
            _connectionString = PrepareConnectionString(connectionString);
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private string PrepareConnectionString(ConnectionString connectionString)
            => string.Concat(
                "Server=", connectionString.Host, ";", 
                "Database=" + connectionString.Database, ";", 
                "Trusted_Connection=" + connectionString.TrustedConnection, ";",
                "User Id=", connectionString.UserId, ";",
                "Password=" + connectionString.Password + ";");
    }
}