using System;
using Serilog;

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
            CheckMetadata(metadata);

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            _logger.Information($"{message} {{ActionId}} {{UserId}}", metadata.ActionId, metadata.UserId);
        }

        public void Error(string errorMessage, LogMetadata metadata)
        {
            CheckMetadata(metadata);

            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentNullException(nameof(errorMessage));

            _logger.Error($"{errorMessage} {{ActionId}} {{UserId}}", metadata.ActionId, metadata.UserId);
        }

        private void CheckMetadata(LogMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));

            if (metadata.ActionId == Guid.Empty)
                throw new ArgumentNullException(nameof(metadata.ActionId));
        }
    }

    public class LogMetadata
    {
        public Guid ActionId { get; set; }

        public int UserId { get; set; }
    }
}
