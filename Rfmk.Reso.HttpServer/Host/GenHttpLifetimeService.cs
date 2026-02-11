using GenHTTP.Api.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Rfmk.Reso.HttpServer.Host;

public class GenHttpLifetimeService(IServerHost serverHost, ILogger<GenHttpLifetimeService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await serverHost.StartAsync();
        logger.LogInformation("HTTP server started, bound to {endpoints}",
            string.Join(", ", serverHost.Instance?.EndPoints.Select(FormatEndpoint) ?? []));
    }

    private static string FormatEndpoint(IEndPoint endpoint)
    {
        var protocol = endpoint.Secure ? "https" : "http";

        var address = endpoint.Address?.ToString() ?? "localhost";

        var port = endpoint.Secure ?
            endpoint.Port == 443 ? "" : $":{endpoint.Port}" :
            endpoint.Port == 80 ? "" : $":{endpoint.Port}";

        return $"{protocol}://{address}{port}";
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await serverHost.StopAsync();
        logger.LogInformation("HTTP server shut down");
    }
}