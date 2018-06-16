using System;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ElasticSearchExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            var sinkOptions = new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
            };
            var loggerConfig = new LoggerConfiguration()
                .WriteTo
                .Elasticsearch(sinkOptions);

            Log.Logger = loggerConfig.CreateLogger();

            Log.Information("Hello, Serilog!");

            Log.CloseAndFlush();
        }
    }
}
