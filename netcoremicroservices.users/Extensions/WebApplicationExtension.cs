
using NetCoreMicroservices.Users.Repositories;
using NetCoreMicroservices.Users.Domains;
using NetCoreMicroservices.Commons.Services;
using System.Security.Claims;
using NetCoreMicroservices.Commons.Middlewares;

namespace NetCoreMicroservices.Users.Extensions
{

    public static class WebApplicationExtension
    {

        public static IApplicationBuilder ConfigureAppBuilder(this WebApplication app)
        {
            //app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();


            app.MapGet("/api/users", async (IWrapperRepository wrapper) =>
            {
                return await Task.FromResult(
                    wrapper.User.FindAll()
                );
            })
            .Produces<UserDomain>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "API Info",
                Description = "Minimal API endpoint"
            });



            //            app.MapGet("/api/cars", () =>
            //            {

            //                var forecast = Enumerable.Range(1, 5).Select(index =>
            //                    new WeatherForecast
            //                    (
            //                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //                        Random.Shared.Next(-20, 55),
            //                        summaries[Random.Shared.Next(summaries.Length)]
            //                    ))
            //                    .ToArray();
            //                return forecast;
            //            })
            //.WithName("GetWeatherForecast")
            //.WithOpenApi();

            return app;
        }
    }
}
