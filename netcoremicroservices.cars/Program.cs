using NetCoreMicroservices.Cars.Extensions;
using NetCoreMicroservices.Commons.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDefaultWebBuilder(builder.ConfigureWebBuilder);

builder.Services.ConfigureDefaultServices(builder.Services.ConfigureServices);

var app = builder.Build();

app.ConfigureDefaultAppBuilder(app.ConfigureAppBuilder);

app.Run();
