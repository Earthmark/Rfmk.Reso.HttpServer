using Microsoft.Extensions.Logging;

namespace Rfmk.Reso.HttpServer.ModBridge;

public class ResoModLogProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
        => new ResoModLogger(categoryName);

    public void Dispose()
    {
    }
}
