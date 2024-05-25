using NetCoreMicroservices.Auth.Context;
using NetCoreMicroservices.Auth.Extensions;
using NetCoreMicroservices.Commons.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDefaultWebBuilder(builder.ConfigureWebBuilder);

builder.Services.ConfigureDefaultServices(builder.Services.ConfigureServices);

var app = builder.Build();

app.ConfigureDefaultAppBuilder();
var dbIni = builder.Services.BuildServiceProvider().GetRequiredService<IDBInitializer>();
app.ConfigureAppBuilder(dbIni);

app.Run();
