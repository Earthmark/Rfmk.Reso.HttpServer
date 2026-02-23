using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Elements.Core;

namespace Rfmk.Reso.Http.LogViewer;

public interface IUniLogListener
{
    IAsyncEnumerable<UniLogMessage> WatchLogs(CancellationToken cancellationToken);
}

public class MockUniLogListener : IUniLogListener
{
    public async IAsyncEnumerable<UniLogMessage> WatchLogs([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            await Task.Delay(1000, cancellationToken);
            yield return new UniLogMessage("Mock log message", UniLogSeverity.Info);
            await Task.Delay(1000, cancellationToken);
            yield return new UniLogMessage("Mock Warning", UniLogSeverity.Warning);
            await Task.Delay(1000, cancellationToken);
            yield return new UniLogMessage("Mock Error Message", UniLogSeverity.Error);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}

public class UniLogListener : IUniLogListener
{
    private static readonly ConcurrentDictionary<Guid, Channel<UniLogMessage>> LogSubjects = new();

    static UniLogListener()
    {
        UniLog.OnWarning += msg => WriteLog(msg, UniLogSeverity.Warning);
        UniLog.OnError += msg => WriteLog(msg, UniLogSeverity.Error);
        UniLog.OnLog += msg => WriteLog(msg, UniLogSeverity.Info);
    }

    private static void WriteLog(string message, UniLogSeverity severity)
    {
        foreach (var channel in LogSubjects.Values)
        {
            channel.Writer.TryWrite(new UniLogMessage(message, severity));
        }
    }

    public async IAsyncEnumerable<UniLogMessage> WatchLogs([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var channel = Channel.CreateUnbounded<UniLogMessage>();
        var id = Guid.NewGuid();
        LogSubjects.TryAdd(id, channel);
        try
        {
            await foreach (var message in channel.Reader.ReadAllAsync(cancellationToken))
            {
                yield return message;
            }
        }
        finally
        {
            LogSubjects.TryRemove(id, out _);
        }
    }
}

public record UniLogMessage(string Message, UniLogSeverity Severity);

public enum UniLogSeverity
{
    Error,
    Warning,
    Info
}
