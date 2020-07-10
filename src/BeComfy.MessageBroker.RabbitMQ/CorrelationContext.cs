using System;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public class CorrelationContext : ICorrelationContext
    {
        public Guid CorrelationId { get; }
        public Guid UserId { get; }
        public Guid ResourceId { get; }
        public string TraceId { get; }
        public string MessageName { get; }
        public string Culture { get; }
        public DateTime CreatedAt { get; }

        public CorrelationContext()
        {
            
        }

        private CorrelationContext(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        private CorrelationContext(Guid correlationId, Guid userId, Guid resourceId, 
            string traceId, string messageName, string culture)
        {
            CorrelationId = correlationId;
            UserId = userId;
            ResourceId = resourceId;
            TraceId = traceId;
            MessageName = messageName;
            Culture = culture;
            CreatedAt = DateTime.UtcNow;
        }

        public ICorrelationContext Empty
            => new CorrelationContext();

        public ICorrelationContext Create(Guid correlationId)
            => new CorrelationContext(correlationId);

        public ICorrelationContext Create<T>(Guid correlationId, Guid userId, Guid resourceId, 
            string traceId, string culture)
            => new CorrelationContext(correlationId, userId, resourceId, traceId, typeof(T).Name, culture);

        public ICorrelationContext From<T>(ICorrelationContext correlationContext)
            => Create<T>(correlationContext.CorrelationId, correlationContext.UserId, correlationContext.ResourceId,
                    correlationContext.TraceId, correlationContext.Culture);
    }
}