using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> ListAllBooksAsync();
    Task<Book> GetBookByIdAsync(int book_id);
    Task<Book> GetBookByNameAsync(int book_id);
    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book, int book_id);
    Task DeleteBookAsync(int book_id);
    Task UpdateQuantityBookAsync(int book_Id, int newQuantity);
    Task<int> GetQuantityBookAsync(int book_id);
}
