using Microsoft.AspNetCore.WebSockets;
using Rfmk.Reso.Http.Kestrel.Components;

namespace Rfmk.Reso.Http.Kestrel;

public class KestrelBuilder
{
    public static WebApplication BuildInstance(bool embedded)
    {
        var injectors = ModServiceInjector.TakeInjectors();

        var builder = WebApplication.CreateBuilder(embedded
            ? new WebApplicationOptions
            {
                ContentRootPath = "rml_mods",
                EnvironmentName = "Development",
                Args = Environment.GetCommandLineArgs(),
            }
            : new WebApplicationOptions
            {
                Args = Environment.GetCommandLineArgs(),
                EnvironmentName = "Development",
            });

        if (embedded)
        {
            builder.Services.AddResoniteContext();
        }

        builder.Services.AddSignalR();
        
        builder.Services.AddWebSockets(o => o.KeepAliveInterval = TimeSpan.FromSeconds(120));

        var assemblies = injectors.Select(i => i.GetType().Assembly).Distinct().ToArray();
        var controllers = builder.Services.AddControllers();
        foreach (var assem in assemblies)
        {
            controllers.AddApplicationPart(assem);
        }

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        foreach (var module in injectors)
        {
            builder.Services.AddSingleton(module);
            if (module is ISubPageHttpModule pageMod)
            {
                builder.Services.AddSingleton(pageMod);
            }
            
            module.AddToBuilder(builder);
        }

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

        app.UseAntiforgery();
        
        app.UseWebSockets();
        
        app.MapStaticResoAssets<App>();
        
        foreach (var module in injectors)
        {
            module.UseInApp(app);
        }
        
        app.MapRazorComponents<App>()
            .AddAdditionalAssemblies(assemblies)
            .AddInteractiveServerRenderMode();

        app.MapControllers();
        
        return app;
    }
}