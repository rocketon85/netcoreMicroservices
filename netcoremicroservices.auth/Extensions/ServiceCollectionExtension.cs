using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NetCoreMicroservices.Auth.Context;
using NetCoreMicroservices.Commons.Extensions;
using NetCoreMicroservices.Commons.Options;
using NetCoreMicroservices.Auth.Context;
using NetCoreMicroservices.Auth.Repositories;
using System.Reflection;

namespace NetCoreMicroservices.Auth.Extensions
{
    public static class ServiceCollectionExtension
    {
       
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var connectionOption = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionOption>>().Value;
            var jwtOption = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOption>>().Value;
            
            services.AddConfigureAuthentication(null, jwtOption);

            services.AddDbContext<AppDbContext>(connectionOption, typeof(Program).GetTypeInfo().Assembly.GetName().Name);


            services.AddScoped<IDBInitializer, DBInitializer>();

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.TryAddTransient<IWrapperRepository, WrapperRepository>();

            return services;
        }


    }
}
