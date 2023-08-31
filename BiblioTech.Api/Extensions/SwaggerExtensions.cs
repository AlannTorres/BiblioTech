using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace BiblioTech.Api.Extensions;

public static class SwaggerExtensions
{
    public static void SwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "BiblioTech",
                Description = "API Checkout",
                TermsOfService = new Uri("https://example.com/terms")
            });

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Beared token",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.OperationFilter<SecurityRequirementsOperationFilter>();

            var xmlApiPath = Path.Combine(Directory.GetCurrentDirectory(), $"{typeof(Program).Assembly.GetName().Name}.xml");

            c.IncludeXmlComments(xmlApiPath);
        });
    }
}
