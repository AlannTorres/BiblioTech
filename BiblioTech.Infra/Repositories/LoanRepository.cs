using BiblioTech.Domain;
using BiblioTech.Domain.Models;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;
using Dapper;

namespace BiblioTech.Infra.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly IDbConnector _dbConnector;

    public LoanRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateBookLoanAsync(BookLoan bookLoan)
    {
        string sql = @"INSERT INTO BookLoans (loan_id, book_id, devolution_Date, loan_Status)
                    VALUES (@loan_id, @book_id, @devolution_Date, @loan_Status)";

        var param = new
        {
            loan_id = bookLoan.Loan.Id,
            book_id = bookLoan.Book.Id,
            devolution_Date = bookLoan.Devolution_Date,
            loan_Status = bookLoan.Loan_Status.ToString()
        };

        await _dbConnector.DbConnection.ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task<List<BookLoan>> GetBookLoanByLoanIdAsync(string bookloan_id)
    {
        string sql = @"SELECT BL.*, L.*, B.*
                       FROM BookLoans BL
                       INNER JOIN Loans L ON BL.loan_id = L.id
                       INNER JOIN Books B ON BL.book_id = B.id
                       WHERE BL.loan_id = @bookloan_id";

        var bookLoans = await _dbConnector.DbConnection
            .QueryAsync<BookLoan, Book, BookLoan>(
                sql: sql,
                map: (bookloan, book) =>
                {
                    bookloan.Book = book;
                    return bookloan;
                },
                param: new { bookloan_id },
                splitOn: "id",
                transaction: _dbConnector.DbTransaction);

        return bookLoans.ToList();
    }

    public async Task CreateLoanAsync(Loan loan)
    {
        string sql = @"INSERT INTO Loans (user_email, employee_email, loan_Date)
                       OUTPUT INSERTED.id
                       VALUES (@user_email, @employee_email, @load_Date)";

        var param = new
        {
            user_email = loan.User.Email,
            employee_email = loan.Employee.Email,
            load_Date = loan.Loan_Date
        };

        string loan_id = await _dbConnector.DbConnection
            .ExecuteScalarAsync<string>(sql, param, _dbConnector.DbTransaction);

        if (loan.Books.Any())
        {
            foreach (var book in loan.Books)
            {
                book.Loan.Id = loan_id;
                await CreateBookLoanAsync(book);
            }
        }
    }

    public async Task<bool> ExistLoanByUserEmail(string user_email)
    {
        string sql = @"SELECT 1 FROM Loans L
                      INNER JOIN Users U ON L.user_email= U.email
                      WHERE U.email = @user_email";

        var loan = await _dbConnector.DbConnection
            .QueryAsync<bool>(sql, new { user_email }, _dbConnector.DbTransaction);

        return loan.FirstOrDefault();
    }

    public async Task<List<Loan>> ListAllLoanByFilterAsync(string? user_name = null)
    {
        string sql = @"SELECT L.*, U.*, E.*
                       FROM Loans L
                       INNER JOIN Users U ON L.user_email = U.email
                       INNER JOIN Users E ON L.employee_email = E.email";

        if (!string.IsNullOrEmpty(user_name)) { sql += " WHERE U.name LIKE @user_name"; }

        var loans = await _dbConnector.DbConnection
            .QueryAsync<Loan, User, User, Loan>(
                sql: sql,
                map: (loan, user, employee) =>
                {
                    loan.User = user;
                    loan.Employee = employee;
                    return loan;
                },
                param: new { user_name = $"%{user_name}%" },
                splitOn: "id",
                transaction: _dbConnector.DbTransaction);

        if (loans.Any())
        {
            foreach (var loan in loans)
            {
                var books = await GetBookLoanByLoanIdAsync(loan.Id);
                loan.Books = books;
            }
        }

        return loans.ToList();
    }

    public async Task<List<BookLoan>> ListBookLoanByLoanIdAsync(string loan_id)
    {
        string sql = @"SELECT BL.*, B.*, L.*
                       FROM BookLoans BL
                       INNER JOIN Books B ON BL.book_id = B.id
                       INNER JOIN Loans L ON BL.loan_id = L.id
                       WHERE BL.loan_id = @loan_id";

        var bookLoans = await _dbConnector.DbConnection
            .QueryAsync<BookLoan, Loan, Book, BookLoan>(
            sql: sql, 
            map: (bookloan, loan, book) => {
                bookloan.Loan = loan;
                bookloan.Book = book;
                return bookloan;
            },
            param: new { loan_id }, 
            splitOn: "id",
            transaction: _dbConnector.DbTransaction);

        return bookLoans.ToList();
    }

    public async Task RegisterReturnBookLoanAsync(string user_email, string book_id)
    {
        string sql = @"UPDATE BL SET BL.loan_Status = 'returned'
                        FROM BookLoans BL
                        INNER JOIN Loans L ON BL.loan_id = L.id
                        INNER JOIN Users U ON L.user_email = U.email
                        WHERE U.email = @user_email 
                        AND BL.book_id = @book_id 
                        AND BL.loan_Status = 'pending'";

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, new { user_email, book_id }, _dbConnector.DbTransaction);
    }
}
