using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using Microsoft.Extensions.Options;
using static BeComfy.Common.RestEase.RestEaseOptions;

namespace BeComfy.Common.RestEase
{
    public static class Extensions
    {
        private static readonly string restEaseSection = "restEase";
        public static void RegisterRestClientFor<T>(this IServiceCollection services, string serviceName)
            where T : class
        {
            IConfiguration config;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }
            
            var restEaseOptions = config.GetOptions<RestEaseOptions>(restEaseSection);
            services.Configure<RestEaseOptions>(config.GetSection(restEaseSection));

            services.AddTransient<T>(c => new RestClient(
                BuildServiceUrl(restEaseOptions.Services.SingleOrDefault(x => x.Name == serviceName)))
            {
                RequestQueryParamSerializer = new QueryParamSerializer()
            }.For<T>());
        }

        private static Uri BuildServiceUrl(RestEaseService service)
            => new UriBuilder() 
                {
                    Scheme = service.Scheme,
                    Host = service.Host,
                    Port = service.Port
                }.Uri;
    }
}