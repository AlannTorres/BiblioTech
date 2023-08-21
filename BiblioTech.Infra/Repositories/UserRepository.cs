using BiblioTech.Domain;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;
using Dapper;

namespace BiblioTech.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnector _dbConnector;

    public UserRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateAsync(User user)
    {
        string sql = @"INSERT INTO
                       Users (Password, Name, CPF, Email, Telephone, Address) 
                       VALUES (@Password, @Name, @CPF, @Email, @Telephone, @Address)";

        var param = new
        {
            user.Password,
            user.Name,
            user.CPF,
            user.Email,
            user.Telephone,
            user.Address
        };

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.Dbtransaction);
    }

    public async Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int user_id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByCpfAsync(string Cpf)
    {
        string sql = "SELECT 1 FROM Users WHERE CPF = @Cpf";

        var user = await _dbConnector.DbConnection.
            QueryAsync<bool>(sql, new { Cpf }, _dbConnector.Dbtransaction);

        return user.FirstOrDefault();
    } // Feito

    public async Task<User> GetUserByIdAsync(int user_id)
    {
        string sql = "SELECT * FROM Users WHERE id = @user_id";

        var user = await _dbConnector.DbConnection.
            QuerySingleAsync<User>(sql, new { user_id }, _dbConnector.Dbtransaction);

        return user;
    } // Feito

    public async Task<List<BookCheckout>> ListAllBooksCheckoutUserAsync(int user_id)
    {
        string sql = @"
             SELECT 
                 bc.id,
                 bc.checkout_Date ,
                 bc.due_Date,
                 bc.status_Checkout,
                 b.id,
                 b.title,
                 u.id
             FROM Books b
             INNER JOIN BooksCheckout bc ON b.id = bc.book_id
             INNER JOIN Users u ON bc.user_id = u.id
             WHERE bc.user_id = @user_id
             ORDER BY bc.due_Date";

        var booksCheckout = await _dbConnector.DbConnection.QueryAsync<BookCheckout, Book, User, BookCheckout>(
            sql: sql,
            map: (bookCheckout, book, user) =>
            {
                bookCheckout.Book = book;
                bookCheckout.User = user;
                return bookCheckout;
            },
            param: new { user_id },
            splitOn: "id",
            transaction: _dbConnector.Dbtransaction);

        return booksCheckout.ToList();
    }

    public async Task<List<BookReserve>> ListAllBooksReserveUserAsync(int user_id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<object>> VerifyUserPendingAsync(int user_id)
    {
        throw new NotImplementedException();
    }
}
