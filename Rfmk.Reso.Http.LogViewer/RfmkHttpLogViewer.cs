using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ResoniteModLoader;

namespace Rfmk.Reso.Http.LogViewer;

public class RfmkHttpLogViewer : ResoniteMod, IResoHttpModule
{
    public override string Name => "Rfmk.Reso.Http.LogViewer";
    public override string Author => "Earthmark";
    public override string Version => "0.1.0";

    public override void OnEngineInit()
    {
        this.RegisterHttpModule();
    }

    public void AddToBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IUniLogListener, UniLogListener>();
    }

    public void UseInApp(WebApplication app)
    {
    }
}
