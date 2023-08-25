using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interfaces.Services;

public interface IReserveService
{
    Task<Response> CreateReserveAsync(Reserve bookReserve);
    Task<Response> CloseReserveAsync(string user_email, string book_id);
    Task<Response<List<Reserve>>> ListAllBookReserveByFilterAsync(string book_name = null, string user_name = null);
}
