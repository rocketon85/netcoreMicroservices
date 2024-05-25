using Microsoft.AspNetCore.ResponseCompression;
using Ocelot.DependencyInjection;

namespace NetCoreMicroservices.APIGateway.Extensions
{
    public static class ServiceCollectionExtension
    {
       
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddOcelot();


            return services;
        }


    }
}
