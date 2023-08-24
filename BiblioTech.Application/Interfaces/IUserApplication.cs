using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Interfaces;

public interface IUserApplication
{
    Task<Response<AuthResponse>> AuthAsync(AuthRequest auth);
    Task<Response> CreateAsync(CreateUserRequest user);
    Task<Response<UserResponse>> GetByIdAsync(string user_id);
    Task<Response<List<BookCheckoutResponse>>> ListBooksCheckoutUser(string user_id);
    Task<Response<List<UserResponse>>> ListByFilterAsync(string user_id = null, string name = null);
}