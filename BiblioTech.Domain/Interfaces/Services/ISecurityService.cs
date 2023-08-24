using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interfaces.Services;

public interface ISecurityService
{
    Task<Response<bool>> VerifyPassword(string password, User user);
    Task<Response<string>> EncryptPassword(string password);
}
