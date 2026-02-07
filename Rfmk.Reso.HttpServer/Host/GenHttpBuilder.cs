using GenHTTP.Api.Content;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Modules.DependencyInjection;
using GenHTTP.Modules.Practices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Rfmk.Reso.HttpServer.Host;

public class GenHttpBuilder(
    IHostEnvironment hostEnv,
    ServerLogCompanion logCompanion,
    IServiceProvider serviceProvider,
    IHandlerBuilder handlerBuilder,
    IOptions<GenHttpOptions> options)
{
    public IServerHost Build()
    {
        var host = GenHTTP.Engine.Internal.Host.Create()
                .AddDependencyInjection(serviceProvider)
                .Handler(handlerBuilder)
                .Defaults()
                .Companion(logCompanion)
                .Development(hostEnv.IsDevelopment());

        foreach (var endpoint in options.Value.Endpoints)
        {
            host.Bind(endpoint.Address, endpoint.Port, endpoint.DualStack);
        }

        return host;
    }
}
