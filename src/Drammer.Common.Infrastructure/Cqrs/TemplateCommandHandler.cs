using System.Data;
using Drammer.Common.Cqrs;
using Drammer.Common.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Drammer.Common.Infrastructure.Cqrs;

public abstract class TemplateCommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly IConnectionProvider _connectionProvider;

    private readonly Func<TCommand, string> _generateSql;

    private readonly ILogger<TemplateCommandHandler<TCommand>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateCommandHandler{TCommand}"/> class.
    /// </summary>
    /// <param name="connectionProvider"></param>
    /// <param name="generateSql"></param>
    /// <param name="logger">The logger.</param>
    protected TemplateCommandHandler(
        IConnectionProvider connectionProvider,
        Func<TCommand, string> generateSql,
        ILogger<TemplateCommandHandler<TCommand>> logger)
    {
        _connectionProvider = connectionProvider;
        _generateSql = generateSql;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<int> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var sql = _generateSql(command);
        if (string.IsNullOrWhiteSpace(sql))
        {
            throw new InvalidOperationException("The generated query template is empty.");
        }

        sql = QueryTemplateHelper.RemoveQueryHeader(sql);

        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace("SQL: {SqlQuery}", sql);
        }

        if (!_connectionProvider.ShouldBeDisposed)
        {
            return await ExecuteAsync(_connectionProvider.GetDbConnection(), sql, command, cancellationToken).ConfigureAwait(false);
        }

        using var connection = _connectionProvider.GetDbConnection();
        return await ExecuteAsync(connection, sql, command, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="connection">The Db connection.</param>
    /// <param name="sql">The SQL query.</param>
    /// <param name="command">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    protected abstract Task<int> ExecuteAsync(
        IDbConnection connection,
        string sql,
        TCommand command,
        CancellationToken cancellationToken = default);
}