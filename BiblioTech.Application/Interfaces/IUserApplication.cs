using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Interfaces;

public interface IUserApplication
{
    Task<Response<AuthResponse>> AuthAsync(AuthRequest auth);

    Task<Response> CreateUserEmployeeAsync(CreateUserRequest user);
    Task<Response<List<UserResponse>>> ListEmployeesByFilterAsync(string? user_email = null, string? name = null);
    Task<Response<List<LoanResponse>>> ListAllLoanEmployeeAsync(string user_email);
    Task<Response<List<ReserveResponse>>> ListAllReserveEmployeeAsync(string user_email);

    Task<Response> CreateUserClientAsync(CreateUserRequest user);
    Task<Response<List<UserResponse>>> ListClientsByFilterAsync(string user_email = null, string name = null);
    Task<Response<List<BookLoanResponse>>> ListAllBooksClientAsync(string user_email);
    Task<Response<List<ReserveResponse>>> ListAllReserveClientAsync(string user_email);

    Task<Response> UpdateUserAsync(CreateUserRequest user, string user_email);
    Task<Response> DeleteUserAsync(string user_email);
}