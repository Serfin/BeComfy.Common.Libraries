using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace BeComfy.Logging.Elk
{
    public static class ComfyLoggerExtensions
    {
        public static LoggerConfiguration ConfigureDefaultSettings(this LoggerConfiguration config, 
            ComfyLoggerOptions options)
        {
            if (!Enum.TryParse<LogEventLevel>(options.MinimumLevel, true, out var minimumLevel))
            {
                minimumLevel = LogEventLevel.Information;
            }
                
            config.MinimumLevel.Is(minimumLevel);

            return config;
        }

        public static LoggerConfiguration ConfigureElasticSearch(this LoggerConfiguration config, 
            ComfyLoggerOptions options)
        {
            if (options.Enabled && options.LogStorage == LogStorage.ElasticSearch)
            {
                return config.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(GetConnectionUri(options))
                {
                    IndexFormat = options.LoggerConfig.IndexPattern,
                    InlineFields = true
                });
            }
            
            return config;
        }

        public static LoggerConfiguration ConfigureConsole(this LoggerConfiguration config, 
            ComfyLoggerOptions options)
        {
            if (options.WriteToConsole)
            {
                return config.WriteTo.Console();
            }
            
            return config;
        }

        public static Uri GetConnectionUri(ComfyLoggerOptions options)
            => new UriBuilder(options.LoggerConfig.LoggerConnectionOptions.Scheme, 
                options.LoggerConfig.LoggerConnectionOptions.Url, 
                options.LoggerConfig.LoggerConnectionOptions.Port).Uri;
    }
}