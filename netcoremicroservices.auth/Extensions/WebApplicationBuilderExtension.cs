using NetCoreMicroservices.Commons.Options;

namespace NetCoreMicroservices.Auth.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder ConfigureWebBuilder(this WebApplicationBuilder builder)
        {
            
            builder.Services.Configure<ConnectionOption>(builder.Configuration.GetSection("ConnectionStrings"));
            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtSettings"));

            return builder;
        }
    }
}
