using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Restaurant.API.HealthChecks
{
    public class CloudinaryHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public CloudinaryHealthCheck(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var cloudName = _configuration["Cloudinary:CloudName"];
                
                if (string.IsNullOrEmpty(cloudName))
                {
                    return HealthCheckResult.Unhealthy("Cloudinary configuration is missing.");
                }

                // A simple ping to the Cloudinary API endpoint for the specific cloud name
                // Usually, if the endpoint is reachable, it means Cloudinary service is up.
                var url = $"https://api.cloudinary.com/v1_1/{cloudName}/ping";

                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                
                var response = await client.GetAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy("Cloudinary is reachable.");
                }

                // If unauthorized or other 4xx/5xx it might just mean we didn't auth the ping, 
                // but at least the service is responding, so we might consider it healthy or degraded
                return HealthCheckResult.Degraded($"Cloudinary responded with status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Error occurred while checking Cloudinary health.", ex);
            }
        }
    }
}
