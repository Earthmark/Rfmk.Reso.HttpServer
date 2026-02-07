using System.Net;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Api.Protocol;
using Microsoft.Extensions.Logging;

namespace Rfmk.Reso.HttpServer.Host;

public class ServerLogCompanion(ILogger<ServerLogCompanion> logger) : IServerCompanion
{
    public void OnRequestHandled(IRequest request, IResponse response)
    {
        logger.LogInformation(
            "Request - {ClientIpAddress} - {MethodRawMethod} {TargetPath} - {StatusRawStatus} - {ResponseContentLength}",
            request.Client.IPAddress, request.Method.RawMethod, request.Target.Path, response.Status.RawStatus,
            response.ContentLength ?? 0);
    }

    public void OnServerError(ServerErrorScope scope, IPAddress? client, Exception error)
    {
        logger.LogWarning(error, "Error - {ClientIpAddress} - {Scope}", client, scope);
    }
}
