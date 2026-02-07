using System.Net;
using GenHTTP.Api.Content;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Modules.DependencyInjection;
using GenHTTP.Modules.Practices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Rfmk.Reso.HttpServer.Host;

public class HttpBuilder(
    IHostEnvironment hostEnv,
    IHandlerBuilder project,
    ServerLogCompanion logCompanion,
    IServiceProvider serviceProvider,
    IOptions<HttpOptions> options)
{
    public IServerHost Build()
    {
        var host = GenHTTP.Engine.Internal.Host.Create()
                .AddDependencyInjection(serviceProvider)
                .Handler(project)
                .Defaults()
                .Companion(logCompanion)
                .Development(hostEnv.IsDevelopment());

        foreach (var endpoint in options.Value.Endpoints)
        {
            Console.WriteLine($"Binding to {endpoint.Address}:{endpoint.Port}");
            host.Bind(endpoint.Address, endpoint.Port, endpoint.DualStack);
        }
        
        return host;
    }
}
