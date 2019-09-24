namespace BeComfy.Common.MSSQL
{
    public class ConnectionString
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public bool TrustedConnection { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}