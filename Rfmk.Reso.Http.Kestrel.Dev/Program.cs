using Rfmk.Reso.Http.Kestrel;
using Rfmk.Reso.Http.LinkProxy;
using Rfmk.Reso.Http.LogViewer;

new RfmkHttpLogViewer().OnEngineInit();
new RfmkHttpLinkProxy().OnEngineInit();

var app = KestrelBuilder.BuildInstance(false);

app.Run();
