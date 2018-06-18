using System;
using System.Threading;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ElasticSearchExperiment
{
    class Program
    {
        private static readonly Random _random = new Random();

        static void Main()
        {
            var sinkOptions = new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
            };
            var loggerConfig = new LoggerConfiguration()
                .WriteTo
                .Elasticsearch(sinkOptions);

            var logger = new PortalLogger(loggerConfig.CreateLogger());

            for (var i = 0; i < 10000; i++)
            {
                var logMetadata = new LogMetadata
                {
                    ActionId = Guid.NewGuid(),
                    UserId = _random.Next(111, 123).ToString()
                };

                if (i % 10 == 0)
                    try
                    {
                        var q = i / 0;
                        q.Equals(null);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, logMetadata);
                    }
                else
                    logger.Info($"Hello, Serilog {i} !", logMetadata);

                Thread.Sleep(TimeSpan.FromSeconds(_random.NextDouble()));
            }

            Log.CloseAndFlush();
        }
    }
}
