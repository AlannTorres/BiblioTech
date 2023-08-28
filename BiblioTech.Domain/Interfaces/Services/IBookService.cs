using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Interfaces.Services;

public interface IBookService
{
    Task<Response> CreateBookAsync(Book book);
    Task<Response> UpdateBookAsync(Book book, string book_id);
    Task<Response> DeleteBookAsync(string book_id);
    Task<Response<Book>> GetBookByIdAsync(string book_id);
    Task<Response<List<Book>>> ListAllBooksByFilterAsync(string? book_name = null);
}
