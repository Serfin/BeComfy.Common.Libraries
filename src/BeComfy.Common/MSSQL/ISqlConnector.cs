using System.Data;

namespace BeComfy.Common.MSSQL
{
    public interface ISqlConnector
    {
        IDbConnection CreateConnection();
    }
}