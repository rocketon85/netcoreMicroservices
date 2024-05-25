using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ocelot.Middleware;

namespace NetCoreMicroservices.APIGateway.Extensions
{

    public static class WebApplicationExtension
    {

        public static IApplicationBuilder ConfigureAppBuilder(this WebApplication app)
        {

            app.UseOcelot().GetAwaiter();

            return app;
        }
    }
}
