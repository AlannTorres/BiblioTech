using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookRepository
{
    Task<List<Book>> ListAllBooksByFilterAsync(string book_name = null, string user_name = null);
    Task<Book> GetBookByIdAsync(string book_id);
    Task<Book> GetBookByNameAsync(string book_id);
    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book, string book_id);
    Task DeleteBookAsync(string book_id);
    Task UpdateQuantityBookAsync(string book_Id, int newQuantity);
    Task<int> GetQuantityBookAsync(string book_id);
}
