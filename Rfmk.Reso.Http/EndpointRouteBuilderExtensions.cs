namespace Rfmk.Reso.Http;

public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps static assets from assemblies in the rml_mods folder.
    /// The provided type should be any type in the assembly where the assets were stored (has a staticwebassets.endpoints.json file).
    /// </summary>
    /// <param name="app">The app to extend</param>
    /// <typeparam name="T">A type in the assembly of static assets to load.</typeparam>
    public static void MapStaticResoAssets<T>(this WebApplication app)
    {
        var assemName = typeof(T).Assembly.GetName();
        app.MapStaticAssets($"rml_mods/{assemName.Name}.staticwebassets.endpoints.json");
    }
}