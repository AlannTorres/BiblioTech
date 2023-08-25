using BiblioTech.Domain;

namespace BiblioTech.Interfaces.Repositories;

public interface IBookRepository
{ 
    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book, string book_id);
    Task DeleteBookAsync(string book_id);
    Task UpdateQuantityBookAsync(string book_Id, int newQuantity);
    Task<Book> GetBookByIdAsync(string book_id);
    Task<Book> GetBookByNameAsync(string book_name);
    Task<Book> GetBookByISBNAsync(long book_isbn);
    Task<List<Book>> ListAllBooksByFilterAsync(string book_id = null, string book_name = null);
}
