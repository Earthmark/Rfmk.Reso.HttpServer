using GenHTTP.Api.Content;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Security;
using GenHTTP.Modules.Controllers;
using GenHTTP.Modules.OpenApi;
using GenHTTP.Modules.ApiBrowsing;
using Rfmk.Reso.HttpServer.Controllers;

namespace Rfmk.Reso.HttpServer;

public static class Project
{
    public static IHandlerBuilder Setup()
    {
        return Layout.Create()
            .AddController<DeviceController>("devices")
            .AddOpenApi()
            .AddRedoc(segment: "docs")
            .Add(CorsPolicy.Permissive());
    }
}