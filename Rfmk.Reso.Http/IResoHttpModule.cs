namespace Rfmk.Reso.Http;

public interface IResoHttpModule
{
    void AddToBuilder(WebApplicationBuilder builder);
    void UseInApp(WebApplication app);
}
