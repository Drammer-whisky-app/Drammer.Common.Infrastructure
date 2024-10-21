using System.Data;
using System.Data.Common;

namespace Drammer.Common.Infrastructure.Data;

/// <summary>
/// The db data reader extensions.
/// </summary>
public static class DbDataReaderExtensions
{
    public static string GetString(this DbDataReader row, string columnName) => row.GetString(row.GetOrdinal(columnName));

    public static string? GetNString(this DbDataReader row, string columnName) =>
        row.IsDBNull(columnName) ? null : (string?) row.GetString(columnName);

    public static int? GetInt32(this DbDataReader row, string columnName) => row.GetInt32(row.GetOrdinal(columnName));

    /// <summary>
    /// The get n int.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int? GetNInt32(this DbDataReader row, string columnName) =>
        row.IsDBNull(columnName) ? null : row.GetInt32(columnName);

    public static long GetInt64(this DbDataReader row, string columnName) => row.GetInt64(row.GetOrdinal(columnName));

    public static long? GetNInt64(this DbDataReader row, string columnName) =>
        row.IsDBNull(columnName) ? null : row.GetInt64(columnName);

    /// <summary>
    /// The get n decimal.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// The <see cref="decimal"/>.
    /// </returns>
    public static decimal? GetNDecimal(this DbDataReader row, string columnName)
    {
        return row.IsDBNull(columnName) ? null : (decimal?)row[columnName];
    }

    /// <summary>
    /// The get bool.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool GetBool(this DbDataReader row, string columnName)
    {
        return (bool)row[columnName];
    }

    public static DateTime GetDateTime(this DbDataReader row, string columnName) => row.GetDateTime(row.GetOrdinal(columnName));

    /// <summary>
    /// The get n date time.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime? GetNDateTime(this DbDataReader row, string columnName)
    {
        var dateTime = row.IsDBNull(columnName) ? null : (DateTime?)row[columnName];
        if (dateTime.HasValue)
        {
            dateTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
        }

        return dateTime;
    }

    public static T GetEnum<T>(this DbDataReader row, string columnName)
    {
        if (row.IsDBNull(columnName))
        {
            throw new InvalidOperationException($"Cannot convert null in column `{columnName}` to enum of type `{typeof(T).Name}`");
        }

        var value = row.GetInt32(columnName);
        return (T)Enum.ToObject(typeof(T), value);
    }

    /// <summary>
    /// The get enum.
    /// </summary>
    /// <param name="row">
    /// The row.
    /// </param>
    /// <param name="columnName">
    /// The column name.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T? GetNEnum<T>(this DbDataReader row, string columnName)
    {
        if (row.IsDBNull(columnName))
        {
            return default;
        }

        var value = row.GetInt32(columnName);
        return (T)Enum.ToObject(typeof(T), value);
    }
}