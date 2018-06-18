using System;
using Serilog;
using Serilog.Events;

namespace ElasticSearchExperiment
{
    public class PortalLogger
    {
        private readonly ILogger _logger;

        public PortalLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Info(string message, LogMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            _logger
                .AddMetadata(metadata, LogEventLevel.Information)
                .Information(message);
        }

        public void Error(string errorMessage, LogMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentNullException(nameof(errorMessage));

            _logger
                .AddMetadata(metadata, LogEventLevel.Error)
                .Error(errorMessage);
        }

        public void Error(Exception error, LogMetadata metadata)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            _logger
                .AddMetadata(metadata, LogEventLevel.Error)
                .Error(error, error.Message);
        }
    }

    internal static class PortalLoggerExtensions
    {
        public static ILogger AddMetadata(this ILogger logger, LogMetadata metadata, LogEventLevel level)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            if (metadata.ActionId == Guid.Empty)
                throw new ArgumentNullException(nameof(metadata.ActionId));

            var result = logger
                .ForContext(level, nameof(Environment.MachineName), Environment.MachineName)
                .ForContext(level, nameof(metadata.ActionId), metadata.ActionId);

            if (!string.IsNullOrWhiteSpace(metadata.UserId))
                result = result.ForContext(level, nameof(metadata.UserId), metadata.UserId);

            return result;
        }
    }

    public class LogMetadata
    {
        public Guid ActionId { get; set; }

        public string UserId { get; set; }
    }
}
