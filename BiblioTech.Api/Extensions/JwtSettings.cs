using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BiblioTech.Api.Extensions;

public static class JwtSettings
{
    public static void JWT(this IServiceCollection services, SecurityKey Key)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddPolicyScheme("programevc", "Authorization Bearer or AccessToken", options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                if (context.Request.Headers["Access-Token"].Any())
                {
                    return "Access-Token";
                }

                return JwtBearerDefaults.AuthenticationScheme;
            };
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = "bibliotech",

                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Key,

                // Verify if token is valid
                ValidateLifetime = true,
                RequireExpirationTime = true,

            };

        });
    }
}
