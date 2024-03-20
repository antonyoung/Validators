using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AntonYoung.Validators.Api.HealthChecks
{
    public class PingHealthCheck 
        : IHealthCheck
    {
        private readonly ILogger<PingHealthCheck> _logger;

        public PingHealthCheck(
            ILogger<PingHealthCheck> logger)
        {
            _logger = logger;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{nameof(PingHealthCheck)} request received");
            
            return Task.FromResult(HealthCheckResult.Healthy("A healthy ping result."));
        }
    }
}