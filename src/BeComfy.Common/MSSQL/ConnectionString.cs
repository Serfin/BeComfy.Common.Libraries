namespace BeComfy.Common.MSSQL
{
    public class ConnectionString
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public bool TrustedConnection { get; set; }
    }
}