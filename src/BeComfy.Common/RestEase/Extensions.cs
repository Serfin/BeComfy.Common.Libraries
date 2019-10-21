using Microsoft.Extensions.DependencyInjection;
using RestEase;

namespace BeComfy.Common.RestEase
{
    public static class Extensions
    {
        public static void RegisterRestClientFor<T>(this IServiceCollection services, string url)
            where T : class
        {
            services.AddTransient<T>(c => new RestClient(url).For<T>());
        }
    }
}