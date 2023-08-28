using BiblioTech.Domain.Validations.Base;
using BiblioTech.Domain;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;

namespace BiblioTech.Application.Interfaces;

public interface IBookApplication
{
    Task<Response> CreateBookAsync(CreateBookRequest book);
    Task<Response> UpdateBookAsync(UpdateBookRequest book, string book_id);
    Task<Response> DeleteBookAsync(string book_id);
    Task<Response<List<BookResponse>>> ListAllBooksByFilterAsync(string? book_name = null);
}
