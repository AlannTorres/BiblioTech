using BiblioTech.Application.DataContract.Response;
using BiblioTech.Domain;

namespace BiblioTech.Application.Interfaces.Security;

public interface ITokenManager
{
    Task<AuthResponse> GeneraterTokenAsync(User user);
}
