using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rfmk.Reso.Http.LogViewer;

[ApiController, Route("api/[controller]")]
public class LogController(IUniLogListener logListener, ILogger<LogController> logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Get()
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            return BadRequest();
        }

        using var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

        logger.LogWarning("Log viewer connected.");
        await foreach (var message in logListener.WatchLogs(HttpContext.RequestAborted))
        {
            await using var socketStream = WebSocketStream.CreateWritableMessageStream(socket, WebSocketMessageType.Text);
            await using var writer = new StreamWriter(socketStream);
            await writer.WriteAsync(message.ToString());
        }
        logger.LogWarning("Log viewer disconnected.");

        return Ok();
    }
}