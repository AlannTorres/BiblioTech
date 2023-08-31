using BiblioTech.Domain;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;
using Dapper;

namespace BiblioTech.Infra.Repositories;

public class ReserveRepository : IReserveRepository
{
    private readonly IDbConnector _dbConnector;

    public ReserveRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CloseReserveAsync(string user_email, string book_id)
    {
        string sql = @"UPDATE R SET status_Reserve = 'canceled'
                       FROM Reserves R
                       JOIN Users U ON R.user_email = U.email
                       WHERE U.email = @user_email
                       AND R.book_id = @book_id
                       AND R.status_Reserve = 'active'";

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, new { user_email, book_id }, _dbConnector.DbTransaction);
    }

    public async Task CreateReserveAsync(Reserve bookReserve)
    {
        string sql = @"INSERT INTO Reserves (user_email, book_id, employee_email, reserve_Date, status_Reserve, EstimatedArrival_Date)
                        VALUES (@user_email, @book_id, @employee_email, @reserve_Date, @status_Reserve, @estimatedArrival_Date)";

        var param = new
        {
            user_email = bookReserve.User.Email,
            book_id = bookReserve.Book.Id,
            employee_email = bookReserve.Employee.Email,
            reserve_Date = bookReserve.Reserve_Date,
            status_Reserve = bookReserve.Status_Reserve.ToString(),
            estimatedArrival_Date = bookReserve.EstimatedArrival_Date
        };

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task<bool> ExistReserveByUserEmail(string user_email)
    {
        string sql = @"SELECT 1 FROM Reserves R 
                       INNER JOIN Users U ON R.user_email = U.email
                       WHERE U.email = @user_email";

        var result = await _dbConnector.DbConnection.QueryAsync<bool>(sql, new { user_email }, _dbConnector.DbTransaction);

        return result.SingleOrDefault();
    }

    public async Task<DateTime> GetEstimatedArrivalDateByBookId(string book_id)
    {
        string sql = @"SELECT MIN(devolution_Date) FROM BookLoans 
                       WHERE book_id = @book_id";

        var estimatedArrivalDate = await _dbConnector.DbConnection
            .ExecuteScalarAsync<DateTime?>(sql, new { book_id }, _dbConnector.DbTransaction);

        return estimatedArrivalDate.Value; 
    }

    public async Task<List<Reserve>> ListAllReserveByFilterAsync(string book_name = null, string user_name = null)
    {
        string sql = @"SELECT R.*, B.*, U.*, E.*
                       FROM Reserves R
                       INNER JOIN Books B ON R.book_id = B.id
                       INNER JOIN Users U ON R.user_email = U.email
                       INNER JOIN Users E ON R.employee_email = E.email
                       WHERE 1 = 1";

        if (!string.IsNullOrEmpty(book_name)) { sql += " AND B.title LIKE @book_name"; }

        if (!string.IsNullOrEmpty(user_name)) { sql += " AND U.name LIKE @user_name"; }

        sql += " ORDER BY R.reserve_Date";

        var reserves = await _dbConnector.DbConnection
            .QueryAsync<Reserve, Book, User, User, Reserve>(
                sql: sql,
                map: (reserve, book, user, employee) =>
                {
                    reserve.Book = book;
                    reserve.User = user;
                    reserve.Employee = employee;
                    return reserve;
                },
                param: new { book_name = $"%{book_name}%", user_name = $"%{user_name}%" },
                splitOn: "id",
                transaction: _dbConnector.DbTransaction);

        return reserves.ToList();
    }
}
