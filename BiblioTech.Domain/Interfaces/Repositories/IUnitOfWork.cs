using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;

namespace BiblioTech.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IReserveRepository ReserveRepository { get; }
    ILoanRepository LoanRepository { get; }
    IBookRepository BookRepository { get; }

    IDbConnector DbConnector { get; }

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
