using ResoniteModLoader;

namespace Rfmk.Reso.Http.LogViewer;

public class RfmkHttpLogViewer : ResoniteMod, IResoHttpModule, ISubPageHttpModule
{
    public override string Name => "Rfmk.Reso.Http.LogViewer";
    public override string Author => "Earthmark";
    public override string Version => "0.1.0";
    public string RoutePrefix => "/log";
    public string PageName => "Log Viewer";

    public override void OnEngineInit()
    {
        this.RegisterHttpModule();
    }

    public void AddToBuilder(WebApplicationBuilder builder)
    {
        _ = builder.Services.IsResoniteHosted()
            ? builder.Services.AddSingleton<IUniLogListener, UniLogListener>()
            : builder.Services.AddSingleton<IUniLogListener, MockUniLogListener>();
    }

    public void UseInApp(WebApplication app)
    {
        app.MapStaticResoAssets<RfmkHttpLogViewer>();
    }
}
