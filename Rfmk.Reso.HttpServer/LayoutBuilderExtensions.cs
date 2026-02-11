

using GenHTTP.Api.Infrastructure;
using GenHTTP.Modules.Controllers.Provider;
using GenHTTP.Modules.Conversion.Formatters;
using GenHTTP.Modules.Conversion.Serializers;
using GenHTTP.Modules.DependencyInjection.Infrastructure;
using GenHTTP.Modules.Reflection;
using GenHTTP.Modules.Reflection.Injectors;
using GenHTTP.Modules.Layouting.Provider;
using GenHTTP.Modules.Webservices.Provider;

// ReSharper disable once CheckNamespace
namespace GenHTTP.Modules.Layouting;

public static class LayoutBuilderExtensions
{
    public static LayoutBuilder IndexDependentController<T>(this LayoutBuilder layout,
        InjectionRegistryBuilder? injectors = null, IBuilder<SerializationRegistry>? serializers = null,
        IBuilder<FormatterRegistry>? formatters = null) where T : class
    {
        var builder = new ControllerBuilder();

        builder.Type(typeof(T));
        builder.InstanceProvider(async r => await InstanceProvider.ProvideAsync<T>(r));

        injectors ??= Injection.Default();
        injectors.Add(new DependencyInjector());
        builder.Injectors(injectors);

        if (serializers != null) builder.Serializers(serializers);
        if (formatters != null) builder.Formatters(formatters);

        return layout.Index(builder);
    }

    public static LayoutBuilder IndexDependentService<T>(this LayoutBuilder layout,
        InjectionRegistryBuilder? injectors = null, IBuilder<SerializationRegistry>? serializers = null,
        IBuilder<FormatterRegistry>? formatters = null) where T : class
    {
        var builder = new ServiceResourceBuilder();

        builder.Type(typeof(T));
        builder.InstanceProvider(async r => await InstanceProvider.ProvideAsync<T>(r));

        injectors ??= Injection.Default();
        injectors.Add(new DependencyInjector());
        builder.Injectors(injectors);

        if (serializers != null) builder.Serializers(serializers);
        if (formatters != null) builder.Formatters(formatters);

        return layout.Index(builder);
    }
}