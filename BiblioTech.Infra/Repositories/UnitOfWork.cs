using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;

namespace BiblioTech.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IUserRepository _userRepository;
    private IBookRepository _bookRepository;
    private ILoanRepository _loanRepository;
    private IReserveRepository _reserveRepository;

    public UnitOfWork(IDbConnector dbConnector) => this.DbConnector = dbConnector;

    public IUserRepository UserRepository => 
        _userRepository ??= new UserRepository(DbConnector);

    public IReserveRepository ReserveRepository => 
        _reserveRepository ??= new ReserveRepository(DbConnector);

    public ILoanRepository LoanRepository => 
        _loanRepository ??= new LoanRepository(DbConnector);

    public IBookRepository BookRepository =>
        _bookRepository ??= new BookRepository(DbConnector);

    public IDbConnector DbConnector { get; }

    public void BeginTransaction()
    {
        DbConnector.DbConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
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
