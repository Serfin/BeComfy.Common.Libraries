using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;

namespace BeComfy.Common.RestEase
{
    public static class Extensions
    {
        private static readonly string reastEaseSection = "restEase";
        public static void RegisterRestClientFor<T>(this IServiceCollection services, string serviceName)
            where T : class
        {
            IConfiguration config;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }

            IEnumerable<RestEaseService> restEaseServices = config.GetOptions<List<RestEaseService>>(reastEaseSection);

            services.AddTransient<T>(c => new RestClient(BuildServiceUrl(restEaseServices.SingleOrDefault(x => x.Name == serviceName)))
            {
                RequestQueryParamSerializer = new QueryParamSerializer()
            }.For<T>());
        }

        private static string BuildServiceUrl(RestEaseService service)
            => $"{service.Protocol}://{service.Host}:{service.Port}";
    }
}