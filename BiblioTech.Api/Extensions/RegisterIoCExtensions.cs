using BiblioTech.Application.Applications;
using BiblioTech.Application.Interfaces;
using BiblioTech.Application.Interfaces.Security;
using BiblioTech.Application.Security;
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

        services.AddScoped<IBookApplication, BookApplication>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<IReserveApplication, ReserveApplication>();
        services.AddScoped<IReserveService, ReserveService>();
        services.AddScoped<IReserveRepository, ReserveRepository>();

        services.AddScoped<ILoanApplication, LoanApplication>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<ILoanRepository, LoanRepository>();

        // Commons
        services.AddScoped<ISecurityService, SecurityService>();

        services.AddScoped<ITokenManager, TokenManager>();
    }
}
