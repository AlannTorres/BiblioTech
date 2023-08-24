using System.Data;

namespace BiblioTech.Interfaces.Repositories.DataConnector;

public interface IDbConnector : IDisposable
{
    IDbConnection DbConnection { get; }
    IDbTransaction DbTransaction { get; set; }
}
