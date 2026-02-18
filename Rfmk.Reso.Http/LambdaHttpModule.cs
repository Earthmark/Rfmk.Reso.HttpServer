namespace Rfmk.Reso.Http;

public class LambdaHttpModule(Action<WebApplicationBuilder> builderModifier, Action<WebApplication> appModifier)
    : IResoHttpModule
{
    public void AddToBuilder(WebApplicationBuilder builder) => builderModifier(builder);
    public void UseInApp(WebApplication app) => appModifier(app);
}
