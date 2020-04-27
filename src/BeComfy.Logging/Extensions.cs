using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BeComfy.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseComfyLogger(this IWebHostBuilder builder)
            => builder.UseSerilog((webHostBuilderContext, loggerConfiguration) 
                => {
                    var loggerOptions = new ComfyLoggerOptions();
                    webHostBuilderContext.Configuration.GetSection("logger")
                        .Bind(loggerOptions);

                    loggerConfiguration
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .ConfigureDefaultSettings(loggerOptions)
                        .ConfigureConsole(loggerOptions)
                        .ConfigureElasticSearch(loggerOptions);
                });
    }
}