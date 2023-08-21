using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookReserveRepository
{
    Task<IEnumerable<BookReserve>> ListAllReserveAsync();
    Task CreateReserveAsync(BookReserve bookReserve);
    Task CloseReserveAsync(int reservationId);
}
