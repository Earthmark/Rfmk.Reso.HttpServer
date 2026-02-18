using FrooxEngine;
using ResoniteModLoader;

namespace Rfmk.Reso.Http.Kestrel;

public class RfmkKestrelHttpServer : ResoniteMod
{
    public override string Name => "Rfmk.Reso.Http.Kestrel";
    public override string Author => "Earthmark";
    public override string Version => "0.1.0";

    public override void OnEngineInit()
    {
        Engine.Current.RunPostInit(() =>
        {
            Msg("Constructing Reso Http application builder.");
            var app = KestrelBuilder.BuildInstance(true);
            app.RunAsync();
            Engine.Current.OnShutdown += () => app?.StopAsync();
            Msg("Application started.");
        });
    }
}
