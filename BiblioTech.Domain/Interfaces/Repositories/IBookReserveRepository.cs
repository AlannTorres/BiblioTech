using LibrarySystem_2.Domain;

namespace LibrarySystem_2.Interfaces.Repositories
{
    public interface IBookReserveRepository
    {
        Task<IEnumerable<BookReserve>> ListAllReserveAsync();
        Task<int> InsertBookReserveAsync(BookReserve bookReserve);
        Task<int> CloseReservationAsync(int reservationId);
    }
}
