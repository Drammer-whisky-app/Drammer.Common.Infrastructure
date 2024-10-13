using System.Data;
using System.Data.SqlClient;
using Drammer.Common.Infrastructure.Database;

namespace Drammer.Common.Infrastructure.Sql;

public sealed class SqlServerConnectionProvider : IConnectionProvider
{
    private readonly string _connectionString;

    public SqlServerConnectionProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool ShouldBeDisposed => true;

    public IDbConnection GetDbConnection()
    {
        return new SqlConnection(_connectionString);
    }
}