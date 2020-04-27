using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BeComfy.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseComfyLogger(this IWebHostBuilder builder)
            => builder.UseSerilog((webHostBuilderContext, loggerConfiguration) 
                => {
                    var appOptions = new AppOptions();
                    webHostBuilderContext.Configuration.GetSection("app")
                        .Bind(appOptions);

                    var loggerOptions = new ComfyLoggerOptions();
                    webHostBuilderContext.Configuration.GetSection("logger")
                        .Bind(loggerOptions);

                    loggerConfiguration
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithProperty("applicationName", appOptions.Name)
                        .ConfigureDefaultSettings(loggerOptions)
                        .ConfigureConsole(loggerOptions)
                        .ConfigureElasticSearch(loggerOptions);
                });
    }
}