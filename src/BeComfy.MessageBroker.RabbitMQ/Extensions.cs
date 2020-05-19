using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            configuration.GetSection(rabbitMqSection)
                .Bind(rabbitMqOptions);

            services.AddSingleton<IConnection>(x => 
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = rabbitMqOptions.HostName,
                    Port = rabbitMqOptions.Port,
                    UserName = rabbitMqOptions.UserName,
                    Password = rabbitMqOptions.Password,
                };

                var connection = connectionFactory.CreateConnection();

                if (!rabbitMqOptions.ExchangeOptions.Declare && rabbitMqOptions.ExchangeOptions != null)
                {
                    return connection;
                }

                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(rabbitMqOptions.ExchangeOptions.Name, 
                        rabbitMqOptions.ExchangeOptions.Type,
                        rabbitMqOptions.ExchangeOptions.Durable, 
                        rabbitMqOptions.ExchangeOptions.AutoDelete, 
                        rabbitMqOptions.ExchangeOptions.Arguments);

                    channel.Close();
                }

                return connection;
            });
        }

        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                return serviceProvider.GetService<IConfiguration>();
            }
        }
    }
}