using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Services;

public class SecurityService : ISecurityService
{
    public Task<Response<string>> EncryptPassword(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        return Task.FromResult(Response.OK(passwordHash));
    }

    public Task<Response<bool>> VerifyPassword(string password, User user)
    {
        bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        return Task.FromResult(Response.OK(validPassword));
    }
}
