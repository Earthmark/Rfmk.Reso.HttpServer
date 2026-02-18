namespace Rfmk.Reso.Http.Kestrel.ModBridge;

public class ResoModLogProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
        => new ResoModLogger(categoryName);

    public void Dispose()
    {
    }
}
