using ResoniteModLoader;

namespace Rfmk.Reso.Http.LinkProxy;

public class RfmkHttpLinkProxy : ResoniteMod, IResoHttpModule
{
    public override string Name => "Rfmk.Reso.Http.LinkProxy";
    public override string Author => "Earthmark";
    public override string Version => "0.1.0";

    public override void OnEngineInit()
    {
        this.RegisterHttpModule();
    }

    public void AddToBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddApplicationPart(typeof(RfmkHttpLinkProxy).Assembly);
    }

    public void UseInApp(WebApplication app)
    {
    }
}