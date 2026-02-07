using Cottle;
using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Pages;
using Rfmk.Reso.HttpServer.Plugins;

namespace Rfmk.Reso.HttpServer.Pages;

public class IndexController(IRendererCache cache, IEnumerable<IHttpPlugin> plugins)
{
    public async ValueTask<IResponseBuilder> Index(IRequest request)
    {
        var template = cache.FromAssembly<IndexController>("Index.html");
        var data = new Dictionary<Value, Value>
        {
            ["plugins"] = Value.FromEnumerable(plugins.Select(p => new KeyValuePair<Value, Value>(p.Name, p.RootRoute)))
        };
        return request.GetPage(await template.RenderAsync(data));
    }
}
