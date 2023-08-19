using System.Data;

namespace BiblioTech.Interfaces.Repositories.DataConnector;

public interface IDbConnector : IDisposable
{
    IDbConnection dbConnection { get; set; }
    IDbTransaction dbtransaction { get; set; }
}
