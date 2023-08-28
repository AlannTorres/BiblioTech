using BiblioTech.Domain.Models;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interface.Services;

public interface IUserService
{
    Task<Response<bool>> AutheticationAsync(string password, User user);

    Task<Response> CreateUserAsync(User user);
    Task<Response> UpdateUserAsync(User user, string user_email);
    Task<Response> DeleteUserAsync(string user_email);
    Task<Response<User>> GetUserByEmailAsync(string user_email);

    Task<Response<List<User>>> ListEmployeesByFilterAsync(string user_email = null, string name = null);
    Task<Response<List<Loan>>> ListAllLoanEmployeesAsync(string user_email);
    Task<Response<List<Reserve>>> ListAllReserveEmployeesAsync(string user_email);

    Task<Response<List<User>>> ListClientsByFilterAsync(string user_email = null, string name = null);
    Task<Response<List<BookLoan>>> ListAllBooksClientAsync(string user_email);
    Task<Response<List<Reserve>>> ListAllReserveClientAsync(string user_email);

}
