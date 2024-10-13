namespace Drammer.Common.Infrastructure.QueryStorage;

public interface IQueryStorage
{
    /// <summary>
    /// Gets a query by name.
    /// </summary>
    /// <param name="name">The query name.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<string> GetQueryAsync(string name, CancellationToken cancellationToken = default);
}