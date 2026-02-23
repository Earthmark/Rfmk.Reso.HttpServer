using FrooxEngine;
using Rfmk.Reso.Http;
using Rfmk.Reso.Http.Kestrel.ModBridge;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionResoniteExtensions
{
    /// <summary>
    /// Adds the Resonite context to the service collection.
    /// </summary>
    public static IServiceCollection AddResoniteContext(this IServiceCollection services)
    {
        services.AddSingleton<ResoniteMarker>();
        services.AddSingleton<ILoggerProvider, ResoModLogProvider>();
        services.AddSingleton(Engine.Current);
        return services;
    }
}
