using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookCheckoutRepository
{
    Task<IEnumerable<BookCheckout>> ListAllBooksChekout();
    Task<int> CheckOutBook(BookCheckout bookCheckout, int days);
    Task<int> ReturnBook(int userId, int bookId);
}
