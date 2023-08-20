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

    public async Task<int> CreateAsync(User user)
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

        var cliente = await _dbConnector.DbConnection
            .ExecuteAsync(sql, param, _dbConnector.Dbtransaction);

        return cliente;
    }

    public Task<int> UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int user_id)
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



    public Task<List<object>> ListAllBooksCheckoutUserAsync(int user_id)
    {
        throw new NotImplementedException();
    }

    public Task<List<object>> ListAllBooksReserveUserAsync(int user_id)
    {
        throw new NotImplementedException();
    }

    public Task<List<object>> ListAllBooksUserAsync(int user_id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> ListAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<object>> VerifyUserPendingAsync(int user_id)
    {
        throw new NotImplementedException();
    }
}
