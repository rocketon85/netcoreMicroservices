using NetCoreMicroservices.Cars.Domains;
using NetCoreMicroservices.Cars.Repositories;
using NetCoreMicroservices.Cars.Structures.SecurityStructure;
using NetCoreMicroservices.Commons.Structures.SecurityStructure;
using System.Security.Claims;


namespace NetCoreMicroservices.Cars.SetUp
{
    public static class EndPoints
    {

        public static IApplicationBuilder SetEndpoints(this WebApplication app)
        {
            PolicyTypesStructure policyTypes = new PolicyTypesStructure();

            app.MapGet("/api/cars", async (ClaimsPrincipal user, IWrapperRepository wrapper) =>
            {
                return await Task.FromResult(
                    wrapper.Car.FindAll()
                );
            })
              .Produces<CarDomain>(StatusCodes.Status200OK)
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "API Info",
                  Description = "Minimal API endpoint"
              })
              .RequireAuthorization(policyTypes.ViewList);

            return app;
        }

    }
}
