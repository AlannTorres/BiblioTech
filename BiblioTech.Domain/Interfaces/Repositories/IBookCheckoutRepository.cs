using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookCheckoutRepository
{
    Task<List<BookCheckout>> ListAllBookCheckoutByFilterAsync(string book_name = null, string user_name = null);
    Task CreateCheckoutAsync(BookCheckout bookCheckout, int days);
    Task ResgisterReturnAsync(string user_email, string book_id);
}
