using GenHTTP.Api.Content;
using Rfmk.Reso.HttpServer.Plugins;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class SystemCollectionPluginExtensions
{
    /// <summary>
    /// Adds the Resonite context to the service collection.
    /// </summary>
    internal static IServiceCollection AddPlugins(this IServiceCollection services)
    {
        services.AddTransient<PluginHandlerBuilder>();
        services.AddTransient<IHandlerBuilder>(s =>
            s.GetService<PluginHandlerBuilder>()?.Setup() ??
            throw new InvalidOperationException("PluginHandlerBuilder not registered"));
        return services;
    }

    public static IServiceCollection AddPluginTransient<T>(this IServiceCollection services)
        where T : class, IHttpPlugin
    {
        services.AddTransient<IHttpPlugin, T>();
        return services;
    }

    public static IServiceCollection AddPluginScoped<T>(this IServiceCollection services)
        where T : class, IHttpPlugin
    {
        services.AddScoped<IHttpPlugin, T>();
        return services;
    }

    public static IServiceCollection AddPluginSingleton<T>(this IServiceCollection services)
        where T : class, IHttpPlugin
    {
        services.AddSingleton<IHttpPlugin, T>();
        return services;
    }
}