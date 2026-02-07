using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenHTTP.Testing;
using Rfmk.Reso.HttpServer.Host;
using Rfmk.Reso.HttpServer.Plugins;

namespace Rfmk.Reso.HttpServer.Tests.Controllers;

[TestClass]
public class DeviceControllerTests
{
    [TestMethod]
    public async Task TestGetDevices()
    {
        await using var runner = await TestHost.RunAsync(new PluginHandlerBuilder(null!, []).Setup());

        using var response = await runner.GetResponseAsync("/devices/");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
