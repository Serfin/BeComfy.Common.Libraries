namespace BeComfy.Logging.Elk
{
    public class ComfyLoggerOptions
    {
        public bool Enabled { get; set; }
        public bool WriteToConsole { get; set; }
        public string MinimumLevel { get; set; }
        public LogStorage LogStorage { get; set; }
        public LoggerConfig LoggerConfig { get; set; }
    }
}