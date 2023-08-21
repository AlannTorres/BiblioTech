using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookCheckoutRepository
{
    Task<IEnumerable<BookCheckout>> ListAllBooksChekout();
    Task CreateCheckoutAsync(BookCheckout bookCheckout, int days);
    Task ResgisterReturnAsync(int userId, int bookId);
}
