namespace Drammer.Common.Infrastructure.Sql;

public static class SqlExtensions
{
    public static int ToSqlValue(this bool val)
    {
        return val ? 1 : 0;
    }

    public static string ToSqlValue(this int val)
    {
        return val.ToString();
    }

    public static string ToSqlValue(this int? val)
    {
        return val?.ToString() ?? "NULL";
    }

    public static string ToSqlValue(this decimal val)
    {
        return val.ToString("0.00").Replace(",", ".");
    }

    public static string ToSqlValue(this decimal? val)
    {
        return val?.ToSqlValue() ?? "NULL";
    }

    public static string ToSqlValue(this string? val)
    {
        return val == null ? "NULL" : $"'{val.Replace("'", "''")}'";
    }

    public static string ToSqlValue(this string? val, int maxLength)
    {
        return val == null ? "NULL" : $"'{val[..Math.Min(val.Length, maxLength)].Replace("'", "''")}'";
    }

    public static string ToSqlValue(this DateTime val)
    {
        return $"'{val:yyyy-MM-dd HH:mm:ss}'";
    }

    public static string ToSqlValue(this DateTime? val)
    {
        return val == null ? "NULL" : val.Value.ToSqlValue();
    }
}