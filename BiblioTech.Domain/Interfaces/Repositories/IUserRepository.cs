using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(string user_id);
    Task<User> GetUserByEmailAsync(string user_email);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(string user_id);
    Task<bool> ExistsByCpfAsync(string Cpf);
    Task<List<object>> VerifyUserPendingAsync(string user_id);
    Task<List<User>> ListAllUsersByFilterAsync(string user_id = null, string name = null);
    Task<List<BookCheckout>> ListAllBooksCheckoutUserAsync(string user_id);
    Task<List<BookReserve>> ListAllBooksReserveUserAsync(string user_id);
}
