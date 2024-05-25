using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NetCoreMicroservices.Cars.Context;
using NetCoreMicroservices.Cars.Repositories;
using NetCoreMicroservices.Cars.SetUp;
using NetCoreMicroservices.Commons.Extensions;
using NetCoreMicroservices.Commons.Options;
using System.Reflection;

namespace NetCoreMicroservices.Cars.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var connectionOption = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionOption>>().Value;
            var jwtOption = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOption>>().Value;

            services.AddConfigureAuthentication(Security.SetSecurity(), jwtOption);
            
            services.AddDbContext<AppDbContext>(connectionOption, typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            
            services.TryAddTransient<IWrapperRepository, WrapperRepository>();

            return services;
        }
    }
}
