namespace BeComfy.Common.EFCore
{
    public class SqlServerSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int CommandTimeout { get; set; }
    }
}