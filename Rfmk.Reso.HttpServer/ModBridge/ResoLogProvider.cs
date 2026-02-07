using Microsoft.Extensions.Logging;

namespace Rfmk.Reso.HttpServer.ModBridge;

public class ResoLogProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
        => new ResoLogger(categoryName);

    public void Dispose()
    {
    }
}
