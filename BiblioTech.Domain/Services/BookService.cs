using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Domain.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<Book>> GetBookByIdAsync(string book_id)
    {
        var response = new Response<Book>();

        var data = await _unitOfWork.BookRepository.GetBookByIdAsync(book_id);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"Livro com id: {book_id}, não existe"));
            return response;
        }

        response.Data = data;

        return response;
    }

    public async Task<Response<Book>> GetBookByNameAsync(string book_name)
    {
        var response = new Response<Book>();

        var data = await _unitOfWork.BookRepository.GetBookByNameAsync(book_name);

        if (data.Equals(null))
        {
            response.Report.Add(Report.Create($"Livro com nome: {book_name}, não existe"));
            return response;
        }

        response.Data = data;

        return response;
    }

    public async Task<Response> CreateBookAsync(Book book)
    {
        var response = new Response();

        var validation = new BookValidation();
        var errors = validation.Validate(book).GetErrors();

        if (errors.Report.Count > 0) { return errors; }

        await _unitOfWork.BookRepository.CreateBookAsync(book);

        return response;
    }

    public async Task<Response> DeleteBookAsync(string book_id)
    {
        var response = new Response();

        var exists = await _unitOfWork.BookRepository.GetBookByIdAsync(book_id);

        if (exists.Equals(null))
        {
            response.Report.Add(Report.Create($"Livro com id: {book_id}, não existe!"));
            return response;
        }

        await _unitOfWork.BookRepository.DeleteBookAsync(book_id);

        return response;
    }

    public async Task<Response> UpdateBookAsync(Book book, string book_isbn)
    {
        var response = new Response();
        var validation = new BookValidation();
        var errors = validation.Validate(book).GetErrors();

        if (errors.Report.Count > 0) return errors;

        var exists = await _unitOfWork.BookRepository.GetBookByISBNAsync(book.ISBN);

        if (exists.Equals(null))
        {
            response.Report.Add(Report.Create($"Livro com ISNB: {book_isbn}, não existe!"));
            return response;
        }

        await _unitOfWork.BookRepository.UpdateBookAsync(book, book_isbn);

        return response;
    }

    public async Task<Response> UpdateQuantityBookAsync(string book_id, int newQuantity)
    {
        var response = new Response();

        var exists = await _unitOfWork.BookRepository.GetBookByIdAsync(book_id);

        if (exists.Equals(null))
        {
            response.Report.Add(Report.Create($"Livro com id: {book_id}, não existe!"));
            return response;
        }

        await _unitOfWork.BookRepository.UpdateQuantityBookAsync(book_id, newQuantity);

        return response;
    }

    public async Task<Response<List<Book>>> ListAllBooksByFilterAsync(string book_id = null, string book_name = null)
    {
        var response = new Response<List<Book>>();

        if (!string.IsNullOrWhiteSpace(book_id))
        {
            var exists = await _unitOfWork.BookRepository.GetBookByIdAsync(book_id);

            if (exists.Equals(null))
            {
                response.Report.Add(Report.Create($"Livro com id: {book_id}, não existe!"));
                return response;
            }
        }

        var data = await _unitOfWork.BookRepository.ListAllBooksByFilterAsync(book_name, book_name);
        response.Data = data;

        return response;
    }

}
