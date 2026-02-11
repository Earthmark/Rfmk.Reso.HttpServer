using GenHTTP.Api.Content;
using GenHTTP.Modules.Functional;

namespace Rfmk.Reso.HttpServer.Plugins;

public class TestPlugin : IHttpPlugin
{
    public string Name => "Test";
    public string RootRoute => "test";
    public IHandlerBuilder RouteHandler()
    {
        return Inline.Create().Get(() => "Taco");
    }
}