
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NetCoreMicroservices.Cars.Domains;
using NetCoreMicroservices.Cars.Repositories;
using NetCoreMicroservices.Cars.SetUp;
using NetCoreMicroservices.Commons.Middlewares;
using NetCoreMicroservices.Commons.Structures.SecurityStructure;
using System.Security.Claims;

namespace NetCoreMicroservices.Cars.Extensions
{
    public static class WebApplicationExtension
    {
        public static IApplicationBuilder ConfigureAppBuilder(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.SetEndpoints();

            return app;
        }
    }
}
