using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;

namespace BiblioTech.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IBookReserveRepository BookReserveRepository { get; }
    IBookCheckoutRepository BookCheckoutRepository { get; }
    IBookRepository BookRepository { get; }

    IDbConnector DbConnector { get; }

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
