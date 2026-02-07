using FrooxEngine;
using Microsoft.Extensions.Logging;
using Rfmk.Reso.HttpServer;
using Rfmk.Reso.HttpServer.ModBridge;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionResoniteExtensions
{
    /// <summary>
    /// Adds the Resonite context to the service collection.
    /// </summary>
    public static IServiceCollection AddResoniteContext(this IServiceCollection services, ResoHttpServer mod)
    {
        services.AddSingleton<ILoggerProvider, ResoModLogProvider>();
        services.AddSingleton(mod);
        services.AddSingleton(Engine.Current);
        return services;
    }
}
