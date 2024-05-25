
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreMicroservices.Auth.Context;
using NetCoreMicroservices.Auth.Models;
using NetCoreMicroservices.Auth.Repositories;
using NetCoreMicroservices.Commons.Middlewares;
using NetCoreMicroservices.Commons.Services;

namespace NetCoreMicroservices.Auth.Extensions
{

    public static class WebApplicationExtension
    {

        public static IApplicationBuilder ConfigureAppBuilder(this WebApplication app, [FromServices] IDBInitializer dBInitializer)
        {
            //app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            dBInitializer.Ini();

            app.MapPost("/api/auth", async (LoginModel login, IWrapperRepository wrapper,
                IJwtService jwtService,
                UserManager<IdentityUser> userManager,
                RoleManager<IdentityRole> roleManager,
                SignInManager<IdentityUser> signInManager) =>
            {
                return await Task.FromResult(
                    //await wrapper.Auth.Authenticate(new LoginModel { UserName = "admin", Password = "Admin123*" }, jwtService, userManager, roleManager, signInManager)
                    await wrapper.Auth.Authenticate(login, jwtService, userManager, roleManager, signInManager)
                );
            })
            .Produces<string>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "API Info",
                Description = "Minimal API endpoint"
            });

           
            return app;
        }
    }
}
