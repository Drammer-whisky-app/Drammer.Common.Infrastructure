using System.Data;

namespace Drammer.Common.Infrastructure.Data;

public interface IConnectionProvider
{
    /// <summary>
    /// Indicates whether the provided connection should be disposed after using. For example, connections
    /// created by Entity Framework should not be disposed.
    /// </summary>
    bool ShouldBeDisposed { get; }

    /// <summary>
    /// Gets a disposable new DB connection.
    /// </summary>
    /// <returns></returns>
    IDbConnection GetDbConnection();
}