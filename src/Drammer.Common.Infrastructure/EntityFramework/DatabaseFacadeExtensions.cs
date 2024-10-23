using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Drammer.Common.Infrastructure.EntityFramework;

/// <summary>
/// The database facade extensions.
/// </summary>
public static class DatabaseFacadeExtensions
{
    /// <summary>
    /// Execute parameterized SQL using <see cref="Dapper"/>.
    /// </summary>
    /// <param name="database">The database.</param>
    /// <param name="sql">The SQL.</param>
    /// <param name="parseRowFunc">The parse row function.</param>
    /// <param name="param">Query parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <typeparam name="T">The result type.</typeparam>
    /// <returns>An <see cref="IReadOnlyList{T}"/> containing the result.</returns>
    [ExcludeFromCodeCoverage]
    public static Task<IReadOnlyList<T>> ExecuteReaderAsync<T>(
        this DatabaseFacade database,
        string sql,
        Func<DbDataReader, T?> parseRowFunc,
        object? param = null,
        CancellationToken cancellationToken = default)
    {
        // this connection is managed by EF and will not be closed in this function.
        var connection = database.GetDbConnection();
        return connection.ExecuteReaderAsync(sql, parseRowFunc, param, cancellationToken);
    }

    internal static async Task<IReadOnlyList<T>> ExecuteReaderAsync<T>(
        this DbConnection connection,
        string sql,
        Func<DbDataReader, T?> parseRowFunc,
        object? param = null,
        CancellationToken cancellationToken = default)
    {
        var result = new List<T>();

        DbDataReader? reader = null;
        try
        {
            reader = await connection.ExecuteReaderAsync(
                sql,
                param);

            if (reader.HasRows)
            {
                while(await reader.ReadAsync(cancellationToken))
                {
                    var data = parseRowFunc(reader);
                    if (data != null)
                    {
                        result.Add(data);
                    }
                }
            }
        }
        finally
        {
            if (reader != null)
            {
                await reader.CloseAsync();
            }
        }

        return result.AsReadOnly();
    }
}