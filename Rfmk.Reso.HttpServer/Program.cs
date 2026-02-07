using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rfmk.Reso.HttpServer.Plugins;

//
// GenHTTP Controller Framework Template
//
// URLs:
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

builder.Services.AddGenHttpHost();
builder.Services.AddPlugins();

builder.Services.AddTransient<IHttpPlugin, TestPlugin>();

var app = builder.Build();

app.Run();
