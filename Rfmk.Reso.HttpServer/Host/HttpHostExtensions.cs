using GenHTTP.Api.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Rfmk.Reso.HttpServer.Host;

public static class HttpHostExtensions
{
    public static IServiceCollection AddHttpHost(this IServiceCollection services)
    {
        services.AddOptions<HttpOptions>().BindConfiguration("HttpServer");
        services.AddTransient<HttpBuilder>();
        services.AddTransient<ServerLogCompanion>();
        services.AddSingleton<IServerHost>(srv =>
            srv.GetService<HttpBuilder>()?.Build() ??
            throw new InvalidOperationException("Expected a vaid http builder."));
        services.AddHostedService<GenHttpLifetimeService>();
        return services;
    }
}
