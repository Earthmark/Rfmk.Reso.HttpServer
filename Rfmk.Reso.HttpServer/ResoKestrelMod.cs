using GenHTTP.Api.Content;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResoniteModLoader;
using Rfmk.Reso.HttpServer.Host;
using Rfmk.Reso.HttpServer.ModBridge;

namespace Rfmk.Reso.HttpServer;

// Mod root
// ReSharper disable once UnusedType.Global
public class ResoHttpServerMod : ResoniteMod
{
    public override string Name => "Rfmk.Reso.HttpServer";
    public override string Author => "Earthmark";
    public override string Version => "0.1.0";

    private IHost? _app;
    
    public override void OnEngineInit()
    {
        Msg("Constructing Reso Http application builder.");
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();

        builder.Services.AddResoniteContext(this);
        
        builder.Services.AddHttpHost();
        builder.Services.AddSingleton<IHandlerBuilder>(_ => Project.Setup());

        _app = builder.Build();

        _app.RunAsync();

        Msg("Application started.");
    }
}
