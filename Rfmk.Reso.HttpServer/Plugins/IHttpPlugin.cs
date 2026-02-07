using GenHTTP.Api.Content;

namespace Rfmk.Reso.HttpServer.Plugins;

public interface IHttpPlugin
{
    string Name { get; }
    string RootRoute { get; }
    IHandlerBuilder RouteHandler();
}
