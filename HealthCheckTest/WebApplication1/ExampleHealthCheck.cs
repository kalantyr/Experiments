using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication1
{
    public class ExampleHealthCheck : IHealthCheck
    {
        internal static readonly Random R = new Random();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            switch (R.Next(3))
            {
                case 1:
                    return Task.FromResult(
                        HealthCheckResult.Degraded("Чёт херовато"));
                
                case 2:
                    return Task.FromResult(
                        HealthCheckResult.Unhealthy("Аларм!",
                            new ApplicationException("Связь с БД потеряна"),
                            new Dictionary<string, object> {{ "ERROR", "ORA-666" }}));

                default:
                    return Task.FromResult(
                        HealthCheckResult.Healthy("Всё норм"));
            }
        }
    }

    public class ExampleHealthCheck2 : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            switch (ExampleHealthCheck.R.Next(3))
            {
                case 1:
                    return Task.FromResult(
                        HealthCheckResult.Degraded("Чёт херовато 2"));

                case 2:
                    return Task.FromResult(
                        HealthCheckResult.Unhealthy("Аларм! 2",
                            new ApplicationException("Связь с БД потеряна"),
                            new Dictionary<string, object> { { "ERROR", "ORA-666" } }));

                default:
                    return Task.FromResult(
                        HealthCheckResult.Healthy("Всё норм 2"));
            }
        }
    }
}
