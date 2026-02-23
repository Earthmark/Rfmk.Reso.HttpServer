namespace Rfmk.Reso.Http;

/// <summary>
/// Extensions for the <see cref="IServiceCollection"/> and <see cref="IServiceProvider"/>, normally for testing if Resonite is in the current host.
///
/// Rebooting Resonite can be heavy, this allows mocking resonite dependent services to test modules outside resonite.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static bool IsResoniteHosted(this IServiceProvider provider) =>
        provider.GetService<ResoniteMarker>() != null;
    
    public static bool IsResoniteHosted(this IServiceCollection collection) =>
        collection.Any(srv => srv.ServiceType == typeof(ResoniteMarker));
}
