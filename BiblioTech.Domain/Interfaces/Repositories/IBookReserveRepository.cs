using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookReserveRepository
{
    Task<IEnumerable<BookReserve>> ListAllReserveAsync();
    Task<int> InsertBookReserveAsync(BookReserve bookReserve);
    Task<int> CloseReservationAsync(int reservationId);
}
