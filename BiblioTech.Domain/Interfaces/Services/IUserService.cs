using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interface.Services;

public interface IUserService
{
    Task<Response> AutheticationAsync(User user);
    Task<Response> CreateAsync(User user);
    Task<Response> UpdateAsync(User user);
    Task<Response<User>> GetUserByIdAsync(int user_id);
    Task<Response> DeleteAsync(int user_id);
    Task<Response<List<BookCheckout>>> ListAllBooksCheckoutUserAsync(int user_id);
    Task<Response<List<BookReserve>>> ListAllBooksReserveUserAsync(int user_id);
}
