using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int user_id);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int user_id);
    Task<bool> ExistsByCpfAsync(string Cpf);
    Task<List<object>> VerifyUserPendingAsync(int user_id);
    Task<List<BookCheckout>> ListAllBooksCheckoutUserAsync(int user_id);
    Task<List<BookReserve>> ListAllBooksReserveUserAsync(int user_id);
}
