using BiblioTech.Application.Applications;
using BiblioTech.Application.Interfaces;
using BiblioTech.Application.Interfaces.Security;
using BiblioTech.Application.Security;
using BiblioTech.Domain.Common.Interfaces;
using BiblioTech.Domain.Common.Repositories;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Services;
using BiblioTech.Infra.Repositories;
using BiblioTech.Interfaces.Repositories;

namespace BiblioTech.Api.Extensions;

public static class RegisterIoCExtensions
{
    public static void RegisterIoC(this IServiceCollection services)
    {
        // Settings
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Entities
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IBookApplication, BookAplication>();

        services.AddScoped<IBookReserveApplication, BookReserveApplication>();

        services.AddScoped<IBookCheckoutApplication, BookCheckoutApplication>();

        // Commons
        services.AddScoped<IGenerators, Generators>();

        services.AddScoped<ISecurityService, SecurityService>();

        services.AddScoped<ITokenManager, TokenManager>();
    }
}
