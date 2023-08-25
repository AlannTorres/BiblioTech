using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;

namespace BiblioTech.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IUserRepository _userRepository;
    //private IBookRepository _bookRepository;
    //private IBookCheckoutRepository _bookCheckoutRepository;
    //private IBookReserveRepository _bookReserveRepository;

    public UnitOfWork(IDbConnector dbConnector)
    {
        this.DbConnector = dbConnector;
    }

    public IUserRepository UserRepository => 
        _userRepository ??= new UserRepository(DbConnector);

    public IReserveRepository BookReserveRepository => 
        throw new NotImplementedException();

    public ILoanRepository BookCheckoutRepository => 
        throw new NotImplementedException();

    public IBookRepository BookRepository => 
        throw new NotImplementedException();

    public IDbConnector DbConnector { get; }

    public void BeginTransaction()
    {
        if (DbConnector.DbConnection.State == System.Data.ConnectionState.Open)
        {
            DbConnector.DbTransaction = DbConnector.DbConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }
    }

    public void CommitTransaction()
    {
        if (DbConnector.DbConnection.State == System.Data.ConnectionState.Open)
        {
            DbConnector.DbTransaction.Commit();
        }
    }

    public void RollbackTransaction()
    {
        if (DbConnector.DbConnection.State == System.Data.ConnectionState.Open)
        {
            DbConnector.DbTransaction.Rollback();
        }
    }
}
