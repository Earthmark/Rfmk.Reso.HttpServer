using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Rfmk.Reso.Http.LogViewer;

[ApiController, Route("api/[controller]")]
public partial class LogController(IUniLogListener logListener, ILogger<LogController> logger) : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
    
    [HttpGet, Route("ws")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Get()
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            return BadRequest();
        }

        using var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

        LogViewerConnected(HttpContext.TraceIdentifier);
        await foreach (var message in logListener.WatchLogs(HttpContext.RequestAborted))
        {
            await using var socketStream =
                WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Text);
            await JsonSerializer.SerializeAsync(socketStream, message, JsonOptions, HttpContext.RequestAborted);
        }
        LogViewerDisconnected(HttpContext.TraceIdentifier);

        return Ok();
    }
    
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Log viewer connected: {id}")]
    private partial void LogViewerConnected(string id);
    
    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Log viewer disconnected: {id}")]
    private partial void LogViewerDisconnected(string id);
}