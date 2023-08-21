using BiblioTech.Application.Applications;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Services;
using BiblioTech.Infra.DataConnector;
using BiblioTech.Infra.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;
using BiblioTech.Interfaces.Repositories;

namespace BiblioTech.Api.Extensions;

public static class RegisterIoCExtensions
{
    public static void RegisterIoC(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
