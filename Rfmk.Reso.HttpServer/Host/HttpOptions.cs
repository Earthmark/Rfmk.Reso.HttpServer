using System.Net;

namespace Rfmk.Reso.HttpServer.Host;

public class HttpOptions
{
    public List<HttpEndpoint> Endpoints { get; set; } = [];
}

public class HttpEndpoint
{
    public IPAddress? Address { get; set; }
    
    public ushort Port { get; set; }
    
    public bool DualStack { get; set; } = true;
}
