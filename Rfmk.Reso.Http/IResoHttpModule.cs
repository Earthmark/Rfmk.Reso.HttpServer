namespace Rfmk.Reso.Http;

/// <summary>
/// A module that adds pages or controllers to the HTTP server.
///
/// The module can add things just to the dependency injector, but the most common use case is adding pages and views.
///
/// The assembly will already be scraped for controllers and razor pages.
///
/// If you are adding a user-visible page, also implement <see cref="ISubPageHttpModule"/> to be added to the page index.
/// </summary>
/// <example>
/// interface IMyModule
/// {
///   bool PlayerHeadBig();
/// }
/// 
/// class MyModule(Engine instance)
/// {
///   public bool PlayerHeadBig() => instance.NonRealPath.Player.Head.Big;
/// }
/// 
/// class MyMockModule
/// {
///   public bool PlayerHeadIsBig { get; set; }
///   public bool PlayerHeadBig() = PlayerHeadIsBig;
/// }
///
/// class MyModule : ResoniteMod, IResoHttpModule, ISubPageHttpModule
/// {
///   string RoutePrefix => "big-head";
///   string Name => "Headed-ness";
///   override void OnEngineInit()
///   {
///     this.RegisterHttpModule();
///   }
///   void AddToBuilder(WebApplicationBuilder builder)
///   {
///     _ = builder.services.IsResoniteHosted()
///      ? builder.services.AddTransient&lt;IMyModule, MyModule&gt;()
///      : builder.services.AddTransient&lt;IMyModule, MyMockModule&gt;();
///   }
///   void UseInApp(WebApplication app)
///   {
///     // Add static assets, this override auto-converts if the 
///     app.MapStaticResoAssets&lt;MyModule&lt;();
///   }
/// }
///
/// [ApiController, Route("api/[controller]")]
/// public class BigHeadController(IMyModule module)
/// {
///   [HttpGet]
///   public bool Get()
///   {
///     return module.PlayerHeadBig();
///   }
/// }
///
/// @page "/big-head"
/// @inject IMyModule Module
///
/// &lt;h1&lt;@Module.PlayerHeadBig()&lt;/h1&lt;
/// 
/// </example>
public interface IResoHttpModule
{
    /// <summary>
    /// Modifies the dependency injector to add services or change configuration.
    /// </summary>
    void AddToBuilder(WebApplicationBuilder builder);
    
    /// <summary>
    /// Adds custom middlewares to the app. 
    /// </summary>
    void UseInApp(WebApplication app);
}
