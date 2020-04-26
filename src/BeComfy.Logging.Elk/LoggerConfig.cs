namespace BeComfy.Logging.Elk
{
    public class LoggerConfig
    {
        public string IndexPattern { get; set; }
        public LoggerConnectionOptions LoggerConnectionOptions { get; set; }
    }
}