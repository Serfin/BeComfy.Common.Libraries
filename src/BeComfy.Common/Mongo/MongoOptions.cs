namespace BeComfy.Common.Mongo
{
    public class MongoOptions
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}