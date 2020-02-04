using System;
using BeComfy.Common.Types.Exceptions;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BeComfy.Common.Consul
{
    public static class Extensions
    {
        private readonly static string consulSection = "consul";

        public static void AddConsul(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var consulOptions = configuration.GetOptions<ConsulOptions>(consulSection);
            services.Configure<ConsulOptions>(configuration.GetSection(consulSection));
            services.AddSingleton<IConsulServiceId, ConsulServiceId>();
            services.AddTransient<IConsulClient>(c => 
                new ConsulClient(config => 
            {
                if (!string.IsNullOrEmpty(consulOptions.ConsulAgentAddress))
                {
                    config.Address = new Uri(consulOptions.ConsulAgentAddress);

                    return;
                }

                throw new BeComfyException("consul_address_not_specifed", "Consul address cannot be empty");
            }));
        }

        public static string UseConsul(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var consulOptions = scope.ServiceProvider.GetService<IOptions<ConsulOptions>>().Value;
                var consulClient = scope.ServiceProvider.GetService<IConsulClient>();
                var consulServiceId = scope.ServiceProvider.GetService<IConsulServiceId>().ServiceId;

                var serviceRegistration = new AgentServiceRegistration
                {
                    ID = consulServiceId,
                    Name = consulOptions.ServiceName,
                    Address = consulOptions.ServiceAddress,
                    Port = consulOptions.ServicePort
                };

                if (consulOptions.HealthcheckEnabled)
                {
                    var check = new AgentServiceCheck
                    {
                        Interval = TimeSpan.FromSeconds(consulOptions.PingInterval),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulOptions.DeregisterAfter),
                        HTTP = consulOptions.PingEndpoint
                    };
                    
                    serviceRegistration.Checks = new[] { check };
                }

                consulClient.Agent.ServiceRegister(serviceRegistration);

                return consulServiceId;
            }
        }
    }
}