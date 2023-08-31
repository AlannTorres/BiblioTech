using BiblioTech.Domain;
using BiblioTech.Domain.Models;
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
    
    //Client
    public async Task<List<User>> ListClientsByFilterAsync(string user_email = null, string name = null)
    {
        string sql = @"SELECT * FROM Users WHERE role = 'client'";

        if (!string.IsNullOrEmpty(user_email)) { sql += " AND email = @user_email"; }

        if (!string.IsNullOrEmpty(name)) { sql += " AND name LIKE @name"; }

        var users = await _dbConnector.DbConnection
                        .QueryAsync<User>(sql, new { user_email, name = $"%{name}%" }, _dbConnector.DbTransaction);

        return users.ToList();
    }

    public async Task<List<BookLoan>> ListAllBooksClientAsync(string user_email)
    {
        string sql = @"SELECT BL.*, L.*, B.*, U.*
                       FROM BookLoans BL
                       INNER JOIN Loans L ON BL.loan_id = L.id
                       INNER JOIN Books B ON BL.book_id = B.id
                       INNER JOIN Users U ON L.user_email = U.email
                       WHERE U.email = @user_email
                       AND U.role = 'client' ";

        var bookloans = await _dbConnector.DbConnection
            .QueryAsync<BookLoan, Book, Loan, BookLoan>(
            sql: sql, 
            map: (bookloan, book, loan) =>
            {
                bookloan.Book = book;
                bookloan.Loan = loan;
                return bookloan;
            },
            param: new { user_email },
            splitOn: "id",
            transaction: _dbConnector.DbTransaction);

        return bookloans.ToList();
    }

    public async Task<List<Reserve>> ListAllReserveClientAsync(string user_email)
    {
        string sql = @"SELECT R.*, B.*, U.*, E.*
                       FROM Reserves R
                       INNER JOIN Books B ON R.book_id = B.id
                       INNER JOIN Users U ON R.user_email = U.email
                       INNER JOIN Users E ON R.employee_email = E.email
                       WHERE U.email = @user_email
                       AND U.role = 'client'";

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
                            param: new { user_email }, 
                            splitOn: "id",
                            transaction: _dbConnector.DbTransaction);

        return reserves.ToList();
    }

    // Employee
    public async Task<List<User>> ListEmployeesByFilterAsync(string user_email = null, string name = null)
    {
        string sql = @"SELECT * FROM Users WHERE role = 'employee'";

        if (!string.IsNullOrEmpty(user_email)) { sql += " AND email = @user_email"; }

        if (!string.IsNullOrEmpty(name)) { sql += " AND name LIKE @name"; }

        var users = await _dbConnector.DbConnection
                        .QueryAsync<User>(sql, new { user_email, name = $"%{name}%" }, _dbConnector.DbTransaction);

        return users.ToList();
    }

    public async Task<List<Loan>> ListAllLoanEmployeeAsync(string user_email)
    {
        string sql = @"SELECT L.*, L.id as id_loan, U.*, E.*
                       FROM Loans L
                       INNER JOIN Users U ON L.user_email = U.email
                       INNER JOIN Users E ON L.employee_email = E.email
                       WHERE E.email = @user_email
                       AND E.role = 'employee'";

        var loans = await _dbConnector.DbConnection
            .QueryAsync<Loan, User, User, Loan>(
                sql: sql,
                map: (loan, user, employee) =>
                {
                    loan.User = user;
                    loan.Employee = employee;
                    return loan;
                },
                param: new { user_email },
                splitOn: "id_loan, id",
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

    public async Task<List<Reserve>> ListAllReserveEmployeeAsync(string user_email)
    {
        string sql = @"SELECT R.*, B.*, U.*, E.*
                       FROM Reserves R
                       INNER JOIN Books B ON R.book_id = B.id
                       INNER JOIN Users U ON R.user_email = U.email
                       INNER JOIN Users E ON R.employee_email = E.email
                       WHERE E.email = @user_email
                       AND E.role = 'employee'";

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
                            param: new { user_email },
                            splitOn: "id",
                            transaction: _dbConnector.DbTransaction);

        return reserves.ToList();
    }

    // All User
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

    public async Task CreateUserAsync(User user)
    {
        string sql = @"INSERT INTO
                       Users (passwordHash, name, CPF, email, telephone, adress, role) 
                       VALUES (@passwordHash, @name, @CPF, @email, @telephone, @adress, @role)";

        var param = new
        {
            user.PasswordHash, user.Name,user.CPF, user.Email, user.Telephone, user.Adress, user.Role
        };

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task UpdateUserAsync(User user, string user_email)
    {
        string sql = @"UPDATE Users SET 
                         passwordHash = @passwordHash,
                         name = @name,
                         CPF = @CPF,
                         email = @email,
                         telephone = @telephone,
                         adress = @adress,
                         role = @role
                       WHERE email = @user_email";   

        var param = new
        {
            user.PasswordHash, user.Name, user.CPF, user.Email, user.Telephone, user.Adress, user.Role, user_email
        };

        await _dbConnector.DbConnection
                .ExecuteAsync(sql, param, _dbConnector.DbTransaction);
    }

    public async Task DeleteUserAsync(string user_email)
    {
        string sql = "DELETE FROM Users WHERE email = @user_email";

        await _dbConnector.DbConnection
            .ExecuteAsync(sql, new { user_email }, _dbConnector.DbTransaction);

    } 

    public async Task<bool> ExistsUserByCpfAsync(string Cpf)
    {
        string sql = "SELECT 1 FROM Users WHERE CPF = @Cpf";

        var user = await _dbConnector.DbConnection.
            QueryAsync<bool>(sql, new { Cpf }, _dbConnector.DbTransaction);

        return user.FirstOrDefault();
    }

    public async Task<bool> ExistsUserByEmailAsync(string email)
    {
        string sql = "SELECT 1 FROM Users WHERE email = @email";

        var user = await _dbConnector.DbConnection.
            QueryAsync<bool>(sql, new { email }, _dbConnector.DbTransaction);

        return user.FirstOrDefault();
    }

    public async Task<User> GetUserByIdAsync(string user_id)
    {
        string sql = "SELECT * FROM Users WHERE id = @user_id";

        var user = await _dbConnector.DbConnection.
            QuerySingleAsync<User>(sql, new { user_id }, _dbConnector.DbTransaction);

        return user;
    }

    public async Task<User> GetUserByEmailAsync(string user_email)
    {
        string sql = "SELECT * FROM Users WHERE email = @user_email";

        var user = await _dbConnector.DbConnection.
            QuerySingleAsync<User>(sql, new { user_email }, _dbConnector.DbTransaction);

        return user;
    }

    public async Task<List<BookLoan>> VerifyUserPendingAsync(string user_email)
    {
        string sql = @"SELECT L.id, B.id, BL.loan_Status
                        FROM Loans L
                        INNER JOIN BookLoans BL ON L.id = BL.loan_id
                        INNER JOIN Books B ON BL.book_id = B.id
                        INNER JOIN Users U ON L.user_id = U.id
                        WHERE U.email = @user_email 
                        AND BL.loan_Status = 'pending'";

        var bookLoans = await _dbConnector.DbConnection
                            .QueryAsync<BookLoan>(sql, new { user_email }, _dbConnector.DbTransaction);

        return bookLoans.ToList();
    }

}
