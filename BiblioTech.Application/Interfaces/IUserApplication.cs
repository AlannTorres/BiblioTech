using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Interfaces;

public interface IUserApplication
{
    Task<Response> CreateAsync(CreateUserRequest user);
    Task<Response<UserResponse>> GetByIdAsync(int user_id);
    Task<Response<List<BookCheckoutResponse>>> ListBooksCheckoutUser(int user_id);
}