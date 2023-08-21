using Microsoft.OpenApi.Models;

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

            var xmlApiPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");

            c.IncludeXmlComments(xmlApiPath);
        });
    }
}
