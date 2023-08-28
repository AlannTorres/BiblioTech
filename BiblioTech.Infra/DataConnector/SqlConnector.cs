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

    public IDbTransaction DbTransaction { get;  set; }

    public IDbTransaction BeginTransaction(IsolationLevel isolation)
    {
        if (DbTransaction != null)
        {
            return DbTransaction;
        }

        if (DbConnection.State == ConnectionState.Closed)
        {
            DbConnection.Open();
        }

        return (DbTransaction = DbConnection.BeginTransaction(isolation));
    }

    public void Dispose()
    {
        DbConnection?.Dispose();
        DbTransaction?.Dispose();
    }
}
