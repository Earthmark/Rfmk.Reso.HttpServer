using System.Collections.Concurrent;
using System.Reflection;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Pages;
using GenHTTP.Modules.Pages.Rendering;

namespace Rfmk.Reso.HttpServer;

public interface IRendererCache
{
    TemplateRenderer FromAssembly<T>(string name);
}

public class RendererCache : IRendererCache
{
    private readonly ConcurrentDictionary<(Assembly, string), TemplateRenderer> _cache = new();

    public TemplateRenderer FromAssembly<T>(string name)
    {
        var assembly = typeof(T).Assembly;
        var template = _cache.GetOrAdd((assembly, name), Make);
        return template;
    }

    private static TemplateRenderer Make((Assembly assembly, string name) key) =>
        Renderer.From(Resource.FromAssembly(key.assembly, key.name).Build());
}
