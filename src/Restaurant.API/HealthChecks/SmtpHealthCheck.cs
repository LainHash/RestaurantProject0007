using System.Net.Sockets;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Restaurant.API.HealthChecks
{
    public class SmtpHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public SmtpHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var host = _configuration["EmailSettings:SmtpServer"];
                var portStr = _configuration["EmailSettings:SmtpPort"];

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(portStr) || !int.TryParse(portStr, out int port))
                {
                    return HealthCheckResult.Unhealthy("SMTP configuration is missing or invalid.");
                }

                using var tcpClient = new TcpClient();
                var connectTask = tcpClient.ConnectAsync(host, port, cancellationToken).AsTask();
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

                var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    return HealthCheckResult.Degraded("SMTP connection timeout.");
                }

                if (connectTask.IsFaulted)
                {
                    return HealthCheckResult.Unhealthy("Could not connect to SMTP server.", connectTask.Exception);
                }

                return HealthCheckResult.Healthy("SMTP connection is healthy.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Error occurred while checking SMTP health.", ex);
            }
        }
    }
}
