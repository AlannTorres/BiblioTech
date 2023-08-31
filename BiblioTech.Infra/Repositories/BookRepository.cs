using BiblioTech.Domain;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;
using Dapper;

namespace BiblioTech.Infra.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbConnector _dbConnector;

    public BookRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateBookAsync(Book book)
    {
        string sql = @"INSERT INTO Books (ISBN, title, year_publication, description, quantity, publishing)
                        VALUES (@ISBN, @title, @year_publication, @description, @quantity, @publishing)";

        var param = new
        {
            book.ISBN, book.Title, book.Year_publication,  book.Description,  book.Quantity,  book.Publishing,
        };

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task UpdateBookAsync(Book book, string book_id)
    {
        string sql = @"UPDATE Books SET 
                         ISBN = @ISBN,
                         title = @title,
                         year_publication = @year_publication,
                         description = @description,
                         quantity = @quantity,
                         publishing = @publishing
                       WHERE id = @book_id";

        var param = new
        {
            book.ISBN, book.Title, book.Year_publication, book.Description, book.Quantity, book.Publishing, book_id
        };

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task DeleteBookAsync(string book_id)
    {
        string sql = "DELETE FROM Books WHERE id = @book_id";

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, new { book_id }, _dbConnector.DbTransaction);
    }

    public async Task<Book> GetBookByIdAsync(string book_id)
    {
        string sql = "SELECT * FROM Books WHERE id = @book_id";

        var book = await _dbConnector.DbConnection.QuerySingleAsync<Book>(sql, new { book_id }, _dbConnector.DbTransaction);

        return book;
    }

    public async Task<bool> ExistBookByISBNAsync(string book_isbn)
    {
        string sql = "SELECT 1 FROM Books WHERE ISBN = @book_isbn";

        var book = await _dbConnector.DbConnection.
            QueryAsync<bool>(sql, new { book_isbn }, _dbConnector.DbTransaction);

        return book.FirstOrDefault();
    }

    public async Task<List<Book>> ListAllBooksByFilterAsync(string book_name = null)
    {
        string sql = @"SELECT * FROM Books";

        if (!string.IsNullOrEmpty(book_name)) { sql += " WHERE title LIKE @book_name"; }

        var books = await _dbConnector.DbConnection
                        .QueryAsync<Book>(sql, new { book_name = $"%{book_name}%" }, _dbConnector.DbTransaction);

        return books.ToList();
    }

    public async Task UpdateQuantityBookAsync(string book_id, int newQuantity)
    {
        string sql = "UPDATE Books SET quantity = @newQuantity WHERE id = @book_id";

        await _dbConnector.DbConnection.ExecuteAsync(sql, new { newQuantity, book_id }, _dbConnector.DbTransaction);
    }

    public async Task<int> GetQuantityBookAsync(string book_Id)
    {
        string sql = "SELECT quantity FROM Books WHERE id = @book_Id";

        var quantity = await _dbConnector.DbConnection.QuerySingleOrDefaultAsync<int>(sql, new { book_Id }, _dbConnector.DbTransaction);

        return quantity;
    }
}
