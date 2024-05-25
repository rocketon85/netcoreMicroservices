using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreMicroservices.Commons.Middlewares;
using NetCoreMicroservices.Commons.Options;
using NetCoreMicroservices.Commons.Options.netcoreAPI.Options;
using NetCoreMicroservices.Commons.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NetCoreMicroservices.Commons.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDefaultServices(this IServiceCollection services, Func<IServiceCollection> additionalConfiguration = null)
        {
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy",
                    policy => policy.WithOrigins("http://localhost:49630,http://localhost:49631,http://localhost:49632")
                                    .AllowAnyHeader());

            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddExceptionHandler<ExceptionHandlerMiddleware>();

            services.TryAddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOption>();

            return additionalConfiguration != null ? additionalConfiguration.Invoke() : services;
        }

        public static IServiceCollection AddConfigureAuthentication(this IServiceCollection services, Action<AuthorizationOptions>? authOption, JwtOption? jwtSettings)
        {
            services.AddAuthorization();
            if(authOption  != null)
                services.AddAuthorization(authOption);

            if (jwtSettings == null) 
                return services;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.TryAddTransient<IJwtService, JwtService>();

            return services;
        }

        public static IServiceCollection AddDbContext<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties)] TContext>(this IServiceCollection services, ConnectionOption? connectionOption, string? assemblyName) where TContext : DbContext
        {

            if (connectionOption == null) 
                return services;

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(connectionOption.DefaultConnection,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(assemblyName);

                        //Configuring Connection Resiliency:
                        sqlOptions.
                            EnableRetryOnFailure(maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });
           
            services.AddScoped<DbContext, TContext>();
          
            return services;
        }
    }
}
