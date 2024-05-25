using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMicroservices.Commons.Options;

namespace NetCoreMicroservices.Commons.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder ConfigureDefaultWebBuilder(this WebApplicationBuilder builder, Func<WebApplicationBuilder> additionalConfiguration = null)
        {
            //builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger());

            return additionalConfiguration != null ? additionalConfiguration.Invoke() : builder;
        }
    }
}
