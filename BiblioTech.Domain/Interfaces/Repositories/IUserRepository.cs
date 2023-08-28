using BiblioTech.Domain;
using BiblioTech.Domain.Models;

namespace BiblioTech.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(string user_id);
    Task<User> GetUserByEmailAsync(string user_email);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user, string user_email);
    Task DeleteUserAsync(string user_email);
    Task<bool> ExistsUserByCpfAsync(string Cpf);
    Task<List<BookLoan>> VerifyUserPendingAsync(string user_email);

    Task<List<User>> ListEmployeesByFilterAsync(string user_email = null, string name = null);
    Task<List<Loan>> ListAllLoanEmployeeAsync(string user_email);
    Task<List<Reserve>> ListAllReserveEmployeeAsync(string user_email);

    Task<List<User>> ListClientsByFilterAsync(string user_email = null, string name = null);
    Task<List<BookLoan>> ListAllBooksClientAsync(string user_email);
    Task<List<Reserve>> ListAllReserveClientAsync(string user_email);
}
