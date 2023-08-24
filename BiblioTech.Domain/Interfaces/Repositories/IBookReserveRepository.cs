using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookReserveRepository
{
    Task<List<BookReserve>> ListAllBookReserveByFilterAsync(string book_name = null, string user_name = null);
    Task CreateReserveAsync(BookReserve bookReserve);
    Task CloseReserveAsync(string reserve_id);
}
