using System;
using System.Threading;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ElasticSearchExperiment
{
    class Program
    {
        static void Main()
        {
            var sinkOptions = new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
            };
            var loggerConfig = new LoggerConfiguration()
                .Enrich
                    .With<PortalLogEventEnricher>()
                .WriteTo
                .Elasticsearch(sinkOptions);

            Log.Logger = loggerConfig.CreateLogger();

            var logger = new PortalLogger(Log.Logger);

            for (var i = 0; i < 100; i++)
            {
                var logMetadata = new LogMetadata
                {
                    ActionId = Guid.NewGuid(),
                    UserId = 123
                };

                logger.Info($"Test {i}", logMetadata);

                if (i % 3 == 0)
                    logger.Error($"Hello, Serilog {i} !", logMetadata);
                else
                    logger.Info($"Hello, Serilog {i} !", logMetadata);

                Thread.Sleep(TimeSpan.FromSeconds(0.1));
            }

            Log.CloseAndFlush();
        }
    }
}
