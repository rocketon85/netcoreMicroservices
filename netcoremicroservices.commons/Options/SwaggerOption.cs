namespace NetCoreMicroservices.Commons.Options
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    namespace netcoreAPI.Options
    {
        /// <summary>
        /// Configures the Swagger generation options.
        /// </summary>
        /// <remarks>This allows API versioning to define a Swagger document per API version after the
        /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
        public class SwaggerOption : IConfigureOptions<SwaggerGenOptions>
        {
            
            public SwaggerOption()
            {
            
            }

            /// <inheritdoc />
            public void Configure(SwaggerGenOptions options)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Authorization",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                
            }

        }
    }

}
