using System.Collections.Generic;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public class RabbitMqExchangeOptions
    {
        public bool Declare{ get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}