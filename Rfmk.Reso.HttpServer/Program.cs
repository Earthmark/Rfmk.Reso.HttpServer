using GenHTTP.Api.Content;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rfmk.Reso.HttpServer;
using Rfmk.Reso.HttpServer.Host;

//
// GenHTTP Controller Framework Template
//
// URLs:
//   http://localhost:8080/devices/
//   http://localhost:8080/docs/
//   http://localhost:8080/openapi.json
//
// Framework documentation:
//   https://genhttp.org/documentation/content/frameworks/controllers/
//
// Method definitions:
//   https://genhttp.org/documentation/content/concepts/definitions/
//
// Additional features:
//   https://genhttp.org/documentation/content/
//

var builder = Host.CreateApplicationBuilder();

builder.Services.AddHttpHost();
builder.Services.AddSingleton<IHandlerBuilder>(_ => Project.Setup());

var app = builder.Build();

app.Run();
