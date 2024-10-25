using System.Data;
using System.Data.Common;

namespace Drammer.Common.Infrastructure.Data;

/// <summary>
/// The db data reader extensions.
/// </summary>
public static class DbDataReaderExtensions
{
    /// <summary>
    /// Get the string value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader">The database data reader.</param>
    /// <param name="columnName">The column name.</param>
    /// <returns>A <see cref="string"/>.</returns>
    public static string GetStringValue(this DbDataReader dbDataReader, string columnName) => dbDataReader.GetString(dbDataReader.GetOrdinal(columnName));

    /// <summary>
    /// Get the nullable string value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static string? GetNullableStringValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : (string?) dbDataReader.GetStringValue(columnName);

    /// <summary>
    /// Get the int value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static int GetInt32Value(this DbDataReader dbDataReader, string columnName) => dbDataReader.GetInt32(dbDataReader.GetOrdinal(columnName));

    /// <summary>
    /// Get the nullable int value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static int? GetNullableInt32Value(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : dbDataReader.GetInt32Value(columnName);

    /// <summary>
    /// Get the long value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static long GetInt64Value(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.GetInt64(dbDataReader.GetOrdinal(columnName));

    /// <summary>
    /// Get the nullable long value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static long? GetNullableInt64Value(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : dbDataReader.GetInt64Value(columnName);

    /// <summary>
    /// Get the decimal value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static decimal GetDecimalValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.GetDecimal(dbDataReader.GetOrdinal(columnName));

    /// <summary>
    /// Get the nullable decimal value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static decimal? GetNullableDecimalValue(this DbDataReader dbDataReader, string columnName)
    {
        return dbDataReader.IsDBNull(columnName) ? null : dbDataReader.GetDecimalValue(columnName);
    }

    /// <summary>
    /// Get the boolean value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static bool GetBooleanValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.GetBoolean(dbDataReader.GetOrdinal(columnName));

    /// <summary>
    /// Get the nullable boolean value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static bool? GetNullableBooleanValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : dbDataReader.GetBooleanValue(columnName);

    /// <summary>
    /// Get the date time value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static DateTime GetDateTimeValueAsUtc(this DbDataReader dbDataReader, string columnName)
    {
        var result = dbDataReader.GetDateTime(dbDataReader.GetOrdinal(columnName));
        result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
        return result;
    }

    /// <summary>
    /// Get the nullable date time value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static DateTime? GetNullableDateTimeValueAsUtc(this DbDataReader dbDataReader, string columnName)
    {
        var dateTime = dbDataReader.IsDBNull(columnName) ? null : (DateTime?)dbDataReader[columnName];
        if (dateTime.HasValue)
        {
            dateTime = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
        }

        return dateTime;
    }

    /// <summary>
    /// Get the enum value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T GetEnumValue<T>(this DbDataReader dbDataReader, string columnName)
        where T : struct, Enum
    {
        if (dbDataReader.IsDBNull(columnName))
        {
            throw new InvalidOperationException($"Cannot convert null in column `{columnName}` to enum of type `{typeof(T).Name}`");
        }

        var value = dbDataReader.GetInt32Value(columnName);
        return (T)Enum.ToObject(typeof(T), value);
    }

    /// <summary>
    /// Get the nullable enum value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetNullableEnumValue<T>(this DbDataReader dbDataReader, string columnName)
        where T : struct, Enum
    {
        if (dbDataReader.IsDBNull(columnName))
        {
            return null;
        }

        var value = dbDataReader.GetInt32Value(columnName);
        return (T)Enum.ToObject(typeof(T), value);
    }
}