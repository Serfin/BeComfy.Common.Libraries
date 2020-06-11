namespace BeComfy.MessageBroker.RabbitMQ
{
    public static class MessageBrokerUtilities
    {
        public static string ToKebabCase(this string input)
            => input.ToLowerInvariant().Replace(" ", "-");
    }
}