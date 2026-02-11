using GenHTTP.Api.Content;
using GenHTTP.Modules.ApiBrowsing;
using GenHTTP.Modules.Inspection;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.OpenApi;
using GenHTTP.Modules.Security;
using Microsoft.Extensions.Logging;
using Rfmk.Reso.HttpServer.Pages;

namespace Rfmk.Reso.HttpServer.Plugins;

public class PluginHandlerBuilder(
    ILogger<PluginHandlerBuilder> logger,
    IEnumerable<IHttpPlugin> plugins)
{
    public IHandlerBuilder Setup()
    {
        var layout = Layout.Create().IndexDependentController<IndexController>();

        foreach (var plugin in plugins)
        {
            logger.LogInformation("Registering plugin {Plugin} at /{Route}", plugin.Name, plugin.RootRoute);
            layout.Add(plugin.RootRoute, plugin.RouteHandler());
        }

        return layout
            .AddOpenApi()
            .AddRedoc(segment: "docs")
            .AddInspector()
            .Add(CorsPolicy.Permissive());
    }
}
