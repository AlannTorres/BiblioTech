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
                       Users (passwordHash, name, CPF, email, telephone, address) 
                       VALUES (@passwordHash, @name, @CPF, @email, @telephone, @address)";

        var param = new
        {
            user.PasswordHash,
            user.Name,
            user.CPF,
            user.Email,
            user.Telephone,
            user.Address
        };

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string user_id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByCpfAsync(string Cpf)
    {
        string sql = "SELECT 1 FROM Users WHERE CPF = @Cpf";

        var user = await _dbConnector.DbConnection.
            QueryAsync<bool>(sql, new { Cpf }, _dbConnector.DbTransaction);

        return user.FirstOrDefault();
    } // Feito

    public async Task<User> GetUserByIdAsync(string user_id)
    {
        string sql = "SELECT * FROM Users WHERE id = @user_id";

        var user = await _dbConnector.DbConnection.
            QuerySingleAsync<User>(sql, new { user_id }, _dbConnector.DbTransaction);

        return user;
    } // Feito

    public async Task<List<Loan>> ListAllBooksCheckoutUserAsync(string user_id)
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

        var booksCheckout = await _dbConnector.DbConnection.QueryAsync<Loan, Book, User, Loan>(
            sql: sql,
            map: (bookCheckout, book, user) =>
            {
                bookCheckout.Book = book;
                bookCheckout.User = user;
                return bookCheckout;
            },
            param: new { user_id },
            splitOn: "id",
            transaction: _dbConnector.DbTransaction);

        return booksCheckout.ToList();
    }

    public async Task<List<Reserve>> ListAllBooksReserveUserAsync(string user_id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<object>> VerifyUserPendingAsync(string user_id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> ListByFilterAsync(string user_id = null, string name = null)
    {
        string sql = @"SELECT * FROM Users
                       WHERE 1 = 1";

        if (!string.IsNullOrWhiteSpace(user_id))
            sql += " AND id = @Id";

        if (!string.IsNullOrWhiteSpace(name))
            sql += " AND name like @Name";

        var users = await _dbConnector.DbConnection
            .QueryAsync<User>(sql, new { Id = user_id, Name = $"%{name}%" }, _dbConnector.DbTransaction);

        return users.ToList();
    }

    public async Task<User> GetUserByEmailAsync(string user_email)
    {
        string sql = "SELECT * FROM Users WHERE email = @user_email";

        var user = await _dbConnector.DbConnection.
            QuerySingleAsync<User>(sql, new { user_email }, _dbConnector.DbTransaction);

        return user;
    }
}
