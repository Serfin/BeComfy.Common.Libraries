using System;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public interface ICorrelationContext
    {
        Guid CorrelationId { get; }
        Guid UserId { get; }
        Guid ResourceId { get; }
        string TraceId { get; }
        string MessageName { get; }
        string Culture { get; }
        DateTime CreatedAt { get; }
    }
}