using NetCoreMicroservices.APIGateway.Extensions;
using NetCoreMicroservices.Commons.Extensions;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.ConfigureDefaultServices(builder.Services.ConfigureServices);


var app = builder.Build();

app.ConfigureDefaultAppBuilder(app.ConfigureAppBuilder);


app.Run();
