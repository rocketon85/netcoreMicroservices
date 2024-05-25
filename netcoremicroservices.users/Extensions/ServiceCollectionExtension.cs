using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NetCoreMicroservices.Commons.Extensions;
using NetCoreMicroservices.Commons.Options;
using NetCoreMicroservices.Users.Context;
using NetCoreMicroservices.Users.Repositories;
using System.Reflection;

namespace NetCoreMicroservices.Users.Extensions
{
    public static class ServiceCollectionExtension
    {
       
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var connectionOption = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionOption>>().Value;
            var jwtOption = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOption>>().Value;
            
            services.AddConfigureAuthentication(null, jwtOption);

            services.AddDbContext<AppDbContext>(connectionOption, typeof(Program).GetTypeInfo().Assembly.GetName().Name);

            services.TryAddTransient<IWrapperRepository, WrapperRepository>();

            return services;
        }


    }
}
