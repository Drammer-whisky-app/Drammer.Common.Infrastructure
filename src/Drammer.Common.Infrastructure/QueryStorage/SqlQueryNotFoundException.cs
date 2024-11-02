using System.Diagnostics.CodeAnalysis;

namespace Drammer.Common.Infrastructure.QueryStorage;

[ExcludeFromCodeCoverage]
public sealed class SqlQueryNotFoundException : Exception
{
    public SqlQueryNotFoundException(string queryName)
        : base($"A query with name '{queryName}' is not found.")
    {
    }
}