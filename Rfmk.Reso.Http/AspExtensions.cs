namespace Rfmk.Reso.Http;

/// <summary>
/// Extensions for ASP.NET types, such as route builders or other web-framework dependent types.
/// </summary>
public static class AspExtensions
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
        
        var prefix = app.Services.IsResoniteHosted() ? "rml_mods/" : "";
        
        app.MapStaticAssets($"{prefix}{assemName.Name}.staticwebassets.endpoints.json");
    }
}
