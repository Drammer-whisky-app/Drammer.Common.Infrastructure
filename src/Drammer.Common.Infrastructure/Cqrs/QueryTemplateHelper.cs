using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Drammer.Common.Infrastructure.Cqrs;

internal static class QueryTemplateHelper
{
    internal const string BeginHeader = "-- BEGIN HEADER";
    internal const string EndHeader = "-- END HEADER";

    /// <summary>
    /// Removes the header from the query template.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    /// <exception cref="InvalidSqlQueryException"></exception>
    [return: NotNullIfNotNull(nameof(query))]
    public static string? RemoveQueryHeader(string? query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return query;
        }

        var beginHeaderIndex = query.IndexOf(BeginHeader, StringComparison.OrdinalIgnoreCase);
        if (beginHeaderIndex == -1)
        {
            throw new InvalidSqlQueryException("Query template does not contain a begin header", query);
        }

        var endHeaderIndex = query.IndexOf(EndHeader, StringComparison.OrdinalIgnoreCase);
        if (endHeaderIndex == -1)
        {
            throw new InvalidSqlQueryException("Query template does not contain an end header", query);
        }

        var sb = new StringBuilder();
        if (beginHeaderIndex > 0)
        {
            sb.Append(query[..beginHeaderIndex]);
        }

        sb.Append(query[(endHeaderIndex + EndHeader.Length)..]);

        return sb.ToString().Trim();
    }
}