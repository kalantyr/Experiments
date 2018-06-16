using System;
using Serilog.Core;
using Serilog.Events;

namespace ElasticSearchExperiment
{
    public class PortalLogEventEnricher: ILogEventEnricher
    {
        /// <summary>
        /// Enrich the log event.
        /// </summary>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(new LogEventProperty(nameof(Environment.MachineName), new ScalarValue(Environment.MachineName)));
        }
    }
}
