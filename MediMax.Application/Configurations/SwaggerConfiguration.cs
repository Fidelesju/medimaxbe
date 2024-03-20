using Microsoft.OpenApi.Models;

namespace MediMax.Application.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MediMax",
                    Description = "BackEnd Application"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerEndPoint = configuration["SwaggerSettings:EndPoint"];
                string swaggerRoutePrefix = configuration["SwaggerSettings:Prefix"];
                c.SwaggerEndpoint(swaggerEndPoint, "v1");
                c.RoutePrefix = swaggerRoutePrefix;
            });
        }
    }
}