using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace NetCoreMicroservices.Commons.Extensions
{
    public static class WebApplicationExtension
    {

        public static IApplicationBuilder ConfigureDefaultAppBuilder(this WebApplication app, Func<IApplicationBuilder> additionalConfiguration = null)
        {
            app.UseResponseCompression();
            app.UseCors("corsPolicy");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(o => { });
            //app.UseHttpsRedirection();

            return additionalConfiguration != null ? additionalConfiguration.Invoke() : app;
        }
    }
}
