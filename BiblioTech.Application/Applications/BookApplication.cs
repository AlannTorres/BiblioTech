using AutoMapper;
using BiblioTech.Application.DataContract.Request;
using BiblioTech.Application.DataContract.Response;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain;
using BiblioTech.Domain.Interfaces.Services;
using BiblioTech.Domain.Validations.Base;

namespace BiblioTech.Application.Applications;

public class BookApplication : IBookApplication
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookApplication(IBookService bookService,
        IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task<Response> CreateBookAsync(CreateBookRequest bookRequest)
    {
        try
        {
            var book = _mapper.Map<Book>(bookRequest);

            return await _bookService.CreateBookAsync(book);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }
    }

    public async Task<Response> DeleteBookAsync(string book_id)
    {
        return await _bookService.DeleteBookAsync(book_id);
    }

    public async Task<Response> UpdateBookAsync(UpdateBookRequest bookRequest, string book_id)
    {
        try
        {
            var bookOld = await _bookService.GetBookByIdAsync(book_id);

            var book = _mapper.Map<Book>(bookRequest);

            if (string.IsNullOrWhiteSpace(book.ISBN)) book.ISBN = bookOld.Data.ISBN;

            if (string.IsNullOrWhiteSpace(book.Description)) book.Description = bookOld.Data.Description;

            if (string.IsNullOrWhiteSpace(book.Title)) book.Title = bookOld.Data.Title;

            if (book.Year_publication.Equals(null)) book.Year_publication = bookOld.Data.Year_publication;

            if (string.IsNullOrWhiteSpace(book.Publishing)) book.Publishing = bookOld.Data.Publishing;

            book.Quantity = bookOld.Data.Quantity;

            return await _bookService.UpdateBookAsync(book, book_id);
        }
        catch (Exception ex)
        {
            var response = Report.Create(ex.Message);

            return Response.Unprocessable(response);
        }

    }

    public async Task<Response<List<BookResponse>>> ListAllBooksByFilterAsync(string? book_name = null)
    {
        Response<List<Book>> book = await _bookService.ListAllBooksByFilterAsync(book_name);

        if (book.Report.Any())
            return Response.Unprocessable<List<BookResponse>>(book.Report);

        var response = _mapper.Map<List<BookResponse>>(book.Data);

        return Response.OK(response);
    }

}
