using Microsoft.Extensions.Logging;
using ResoniteModLoader;

namespace Rfmk.Reso.HttpServer.ModBridge;

public class ResoLogger(string categoryName) : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        (logLevel switch
        {
            LogLevel.Trace => (Action<string>)ResoniteMod.Debug,
            LogLevel.Debug => ResoniteMod.Debug,
            LogLevel.Information => ResoniteMod.Msg,
            LogLevel.Warning => ResoniteMod.Warn,
            LogLevel.Error => ResoniteMod.Error,
            LogLevel.Critical => ResoniteMod.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        })?.Invoke($"{categoryName}: {formatter(state, exception)}");
    }

    public bool IsEnabled(LogLevel logLevel) => ResoniteMod.IsDebugEnabled() || logLevel != LogLevel.Trace && logLevel != LogLevel.Debug;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}