using GenHTTP.Api.Infrastructure;
using Rfmk.Reso.HttpServer;
using Rfmk.Reso.HttpServer.Host;
using Rfmk.Reso.HttpServer.Plugins;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionGenHttpHostExtensions
{
    /// <summary>
    /// Adds a GenHTTP host to the service collection.
    ///
    /// An IHandlerBuilder needs to be registered as well.
    /// </summary>
    public static IServiceCollection AddGenHttpHost(this IServiceCollection services)
    {
        services.AddOptions<GenHttpOptions>().BindConfiguration("HttpServer");
        services.AddTransient<GenHttpBuilder>();
        services.AddTransient<ServerLogCompanion>();
        services.AddTransient<PluginHandlerBuilder>();
        services.AddSingleton<IRendererCache, RendererCache>();
        services.AddSingleton<IServerHost>(srv =>
            srv.GetService<GenHttpBuilder>()?.Build() ??
            throw new InvalidOperationException("Expected a valid http builder."));
        services.AddHostedService<GenHttpLifetimeService>();
        return services;
    }
}
