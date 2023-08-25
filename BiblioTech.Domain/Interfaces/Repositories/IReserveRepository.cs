using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IReserveRepository
{
    Task CreateReserveAsync(Reserve bookReserve);
    Task CloseReserveAsync(string user_email, string book_id);
    Task<bool> ExistReserveByUserEmail(string user_email);
    Task<DateTime> GetEstimatedArrivalDateByBookId(string book_id);
    Task<List<Reserve>> ListAllReserveByFilterAsync(string book_name = null, string user_name = null);
}
