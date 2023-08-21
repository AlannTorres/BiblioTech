using System.Data;

namespace BiblioTech.Interfaces.Repositories.DataConnector;

public interface IDbConnector : IDisposable
{
    IDbConnection DbConnection { get; }
    IDbTransaction Dbtransaction { get; set; }
}
