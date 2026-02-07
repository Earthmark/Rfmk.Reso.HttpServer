using FrooxEngine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResoniteModLoader;
using Rfmk.Reso.HttpServer.Plugins;

namespace Rfmk.Reso.HttpServer;

public class ResoHttpServer : ResoniteMod
{
    public override string Name => "Rfmk.Reso.HttpServer";
    public override string Author => "Earthmark";
    public override string Version => "0.1.0";

    private static HostApplicationBuilder? _builderField = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();
    private static bool _onInitCalled;

    /// <summary>
    /// An application builder used to host the HTTP server.
    ///
    /// Inject sub-sites to the builder by adding <see cref="IHttpPlugin"/> implementors.
    ///
    /// The builder can only be modified before <see cref="Engine.RunPostInit"/> has been called,
    /// add services to the builder during <see cref="ResoniteMod.OnEngineInit"/>.
    /// </summary>
    /// <example>
    /// class MyPlugin : IHttpPlugin
    /// {
    ///   public string Name => "MyPlugin";
    ///   public string RootRoute => "/myNewRoute";
    ///   public IHandlerBuilder RouteHandler() => new StaticContentHandler("MyPlugin content");
    /// }
    /// public override void OnEngineInit()
    /// {
    ///   // The service can be injected as any lifetime, if the lifetime is less than Singleton, multiple instances may be created. 
    ///   Builder.Services.AddSingleton&lt;IHttpPlugin&gt;(new MyPlugin());
    /// }
    /// </example>
    /// <exception cref="InvalidOperationException">Engine.RunPostInit has already been called, and the builder has run.</exception>
    public static HostApplicationBuilder Builder =>
        _builderField ?? throw new InvalidOperationException("Cannot access builder after Engine.RunPostInit has been called.");

    public override void OnEngineInit()
    {
        if (_onInitCalled) throw new InvalidOperationException("OnInit called twice, this should not happen in production.");
        _onInitCalled = true;

        Builder.Services.AddGenHttpHost();
        Builder.Services.AddPlugins();

        Builder.Services.AddResoniteContext(this);

        IHost? app = null;

        Engine.Current.RunPostInit(() =>
        {
            var builder = _builderField;
            _builderField = null;
            if (builder == null)
            {
                Error("Failed to start Reso Attp application, builder wasn't available on post-init. Please report this as a bug.");
                return;
            }
            Msg("Constructing Reso Http application builder.");
            app = builder.Build();
            app.RunAsync();
            Msg("Application started.");
        });
        Engine.Current.OnShutdown += () => app?.StopAsync();
    }
}
