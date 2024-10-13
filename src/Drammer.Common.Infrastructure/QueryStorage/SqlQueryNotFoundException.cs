namespace Drammer.Common.Infrastructure.QueryStorage;

public sealed class SqlQueryNotFoundException : Exception
{
    public SqlQueryNotFoundException(string queryName)
        : base($"A query with name '{queryName}' is not found.")
    {
    }
}