using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Rfmk.Reso.HttpServer.ModBridge;

public static class ResoHostExtensions
{
    public static IServiceCollection AddResoniteContext(this IServiceCollection services, ResoHttpServerMod mod)
    {
        services.AddSingleton<ILoggerProvider, ResoLogProvider>();
        services.AddSingleton(mod);
        
        
        return services;
    }
}