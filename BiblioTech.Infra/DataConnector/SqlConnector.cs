using BiblioTech.Interfaces.Repositories.DataConnector;
using System.Data;
using System.Data.SqlClient;

namespace BiblioTech.Infra.DataConnector;

public class SqlConnector : IDbConnector
{
    public SqlConnector(string connectionString)
    {
        DbConnection = SqlClientFactory.Instance.CreateConnection();
        DbConnection.ConnectionString = connectionString;
    }

    public IDbConnection DbConnection { get; }

    public IDbTransaction Dbtransaction { get;  set; }

    public void Dispose()
    {
        DbConnection?.Dispose();
        Dbtransaction?.Dispose();
    }
}
