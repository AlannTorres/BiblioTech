using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interface.Services;

public interface IUserService
{
    Task<Response<bool>> AutheticationAsync(string password, User user);
    Task<Response> CreateAsync(User user);
    Task<Response> UpdateAsync(User user);
    Task<Response<User>> GetUserByEmailAsync(string user_email);
    Task<Response> DeleteAsync(string user_id);
    Task<Response<List<User>>> ListByFilterAsync(string user_id = null, string name = null);
    Task<Response<List<Loan>>> ListAllBooksCheckoutUserAsync(string user_id);
    Task<Response<List<Reserve>>> ListAllBooksReserveUserAsync(string user_id);
}
