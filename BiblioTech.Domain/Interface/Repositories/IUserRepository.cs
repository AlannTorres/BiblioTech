using LibrarySystem_2.Domain;

namespace LibrarySystem_2.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int user_id);
    Task<bool> ExistsByCpfAsync(string Cpf);
    Task<int> InsertAsync(User user);
    Task<int> UpdateAsync(User user);
    Task<int> DeleteAsync(int user_id);
    Task<List<User>> ListAllUsersAsync();
    Task<List<object>> ListAllBooksUserAsync(int user_id);
    Task<List<object>> ListAllBooksReserveUserAsync(int user_id);
    Task<List<object>> ListAllBooksCheckoutUserAsync(int user_id);
    Task<List<object>> VerifyUserPendingAsync(int user_id);
}
