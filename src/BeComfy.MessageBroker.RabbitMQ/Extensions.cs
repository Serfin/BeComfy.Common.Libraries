using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public static class Extensions
    {
        private const string rabbitMqSection = "rabbitMq";

        public static IMessageConsumer UseRabbitMq(this IApplicationBuilder app)
            => new MessageConsumer(app);

        public static void AddRabbitMq(this IServiceCollection services)
        {   
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var rabbitMqOptions = new RabbitMqOptions();
            configuration.GetSection(rabbitMqSection).Bind(rabbitMqOptions);
            services.AddSingleton(rabbitMqOptions);

            services.AddSingleton<IConnection>(serviceProvider => 
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = rabbitMqOptions.HostName,
                    Port = rabbitMqOptions.Port,
                    UserName = rabbitMqOptions.UserName,
                    Password = rabbitMqOptions.Password,
                };

                var connection = connectionFactory.CreateConnection();

                using (var channel = connection.CreateModel())
                {
                    var logger = serviceProvider.GetService<ILogger<IConnection>>();
                    logger.LogInformation($"Declaring an exchange - [{rabbitMqOptions.ExchangeOptions.Name}]");

                    channel.ExchangeDeclare(rabbitMqOptions.ExchangeOptions.Name, 
                        rabbitMqOptions.ExchangeOptions.Type.ToLowerInvariant(),
                        rabbitMqOptions.ExchangeOptions.Durable, 
                        rabbitMqOptions.ExchangeOptions.AutoDelete, 
                        rabbitMqOptions.ExchangeOptions.Arguments);

                    channel.Close();
                }

                return connection;
            });
        }
    }
}