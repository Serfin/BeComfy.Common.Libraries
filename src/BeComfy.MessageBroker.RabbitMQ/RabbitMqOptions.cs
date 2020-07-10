namespace BeComfy.MessageBroker.RabbitMQ
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? LoggerEnabled { get; set; }
        public RabbitMqExchangeOptions ExchangeOptions { get; set; }
        public RabbitMqQueueOptions QueueOptions { get; set; }
    }
}