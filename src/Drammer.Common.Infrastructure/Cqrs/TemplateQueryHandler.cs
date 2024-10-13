using System.Data;
using Drammer.Common.Cqrs;
using Drammer.Common.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Drammer.Common.Infrastructure.Cqrs;

public abstract class TemplateQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    private readonly IConnectionProvider _connectionProvider;

    private readonly Func<TQuery, string> _generateSql;

    private readonly ILogger<TemplateQueryHandler<TQuery, TResult>> _logger;

    protected TemplateQueryHandler(
        IConnectionProvider connectionProvider,
        Func<TQuery, string> generateSql,
        ILogger<TemplateQueryHandler<TQuery, TResult>> logger)
    {
        _connectionProvider = connectionProvider;
        _generateSql = generateSql;
        _logger = logger;
    }

    /// <inheritdoc/>
    public virtual async Task<TResult> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        var sql = _generateSql(query);
        if (string.IsNullOrWhiteSpace(sql))
        {
            throw new InvalidOperationException("The generated query template is empty.");
        }

        // sanitize
        sql = QueryTemplateHelper.RemoveQueryHeader(sql);

        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace("SQL: {SqlQuery}", sql);
        }

        if (!_connectionProvider.ShouldBeDisposed)
        {
            return await ExecuteAsync(_connectionProvider.GetDbConnection(), sql, query, cancellationToken).ConfigureAwait(false);
        }

        using var connection = _connectionProvider.GetDbConnection();
        return await ExecuteAsync(connection, sql, query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the query.
    /// </summary>
    /// <param name="connection">The Db connection.</param>
    /// <param name="sql">The SQL query.</param>
    /// <param name="query">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    protected abstract Task<TResult> ExecuteAsync(
        IDbConnection connection,
        string sql,
        TQuery query,
        CancellationToken cancellationToken = default);
}