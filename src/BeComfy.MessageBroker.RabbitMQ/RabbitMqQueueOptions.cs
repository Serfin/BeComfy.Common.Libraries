using System.Collections.Generic;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public class RabbitMqQueueOptions
    {
        public bool AutoDelete { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}