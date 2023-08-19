using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interface.Services;

public interface IUserService
{
    Task<Response> AutheticationAsync(User user);
    Task<Response> CreateAsync(User user);
    Task<Response> UpdateAsync(User user);
    Task<Response<User>> GetUserByIdAsync(int user_id);
    Task<Response> DeleteAsync(int user_id);
    Task<Response<List<User>>> ListAllUsersAsync();
    Task<Response<List<object>>> ListAllBooksUserAsync(int user_id);
    Task<Response<List<object>>> ListAllBooksReserveUserAsync(int user_id);
    Task<Response<List<object>>> ListAllBooksCheckoutUserAsync(int user_id);
}
