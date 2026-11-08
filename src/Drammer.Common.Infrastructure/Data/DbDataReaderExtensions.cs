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
    public static string GetStringValue(this DbDataReader dbDataReader, string columnName)=>
        dbDataReader.GetRequiredValue(columnName, nameof(String), static (reader, ordinal) => reader.GetString(ordinal));

    /// <summary>
    /// Get the nullable string value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static string? GetNullableStringValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : (string?) dbDataReader.GetStringValue(columnName);

    /// <summary>
    /// Get the GUID value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static Guid GetGuidValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.GetRequiredValue(columnName, nameof(Guid), static (reader, ordinal) => reader.GetGuid(ordinal));

    /// <summary>
    /// Get the nullable GUID value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static Guid? GetNullableGuidValue(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : dbDataReader.GetGuidValue(columnName);

    /// <summary>
    /// Get the int value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static short GetInt16Value(this DbDataReader dbDataReader, string columnName)=>
        dbDataReader.GetRequiredValue(columnName, nameof(Int16), static (reader, ordinal) => reader.GetInt16(ordinal));

    /// <summary>
    /// Get the nullable int value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static short? GetNullableInt16Value(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.IsDBNull(columnName) ? null : dbDataReader.GetInt16Value(columnName);

    /// <summary>
    /// Get the int value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static int GetInt32Value(this DbDataReader dbDataReader, string columnName) =>
        dbDataReader.GetRequiredValue(columnName, nameof(Int32), static (reader, ordinal) => reader.GetInt32(ordinal));

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
        dbDataReader.GetRequiredValue(columnName, nameof(Int64), static (reader, ordinal) => reader.GetInt64(ordinal));

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
        dbDataReader.GetRequiredValue(columnName, nameof(Decimal), static (reader, ordinal) => reader.GetDecimal(ordinal));

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
        dbDataReader.GetRequiredValue(columnName, nameof(Boolean), static (reader, ordinal) => reader.GetBoolean(ordinal));

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
    /// <param name="kind">The <see cref="DateTimeKind"/> to specify for the resulting <see cref="DateTime"/>.</param>
    /// <returns></returns>
    public static DateTime GetDateTimeValueAsUtc(this DbDataReader dbDataReader, string columnName, DateTimeKind kind = DateTimeKind.Utc)
    {
        var result = dbDataReader.GetRequiredValue(columnName, nameof(DateTime), static (reader, ordinal) => reader.GetDateTime(ordinal));
        result = DateTime.SpecifyKind(result, kind);
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
    public static T GetEnumValueFromInt32<T>(this DbDataReader dbDataReader, string columnName)
        where T : struct, Enum
    {
        var value = dbDataReader.GetRequiredValue(columnName, $"enum of type `{typeof(T).Name}`", static (reader, ordinal) => reader.GetInt32(ordinal));
        return (T)Enum.ToObject(typeof(T), value);
    }

    /// <summary>
    /// Get the nullable enum value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetNullableEnumValueFromInt32<T>(this DbDataReader dbDataReader, string columnName)
        where T : struct, Enum
    {
        if (dbDataReader.IsDBNull(columnName))
        {
            return null;
        }

        var value = dbDataReader.GetInt32Value(columnName);
        return (T)Enum.ToObject(typeof(T), value);
    }

    /// <summary>
    /// Get the enum value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static T GetEnumValueFromInt16<T>(this DbDataReader dbDataReader, string columnName)
        where T : struct, Enum
    {
        var value = dbDataReader.GetRequiredValue(columnName, $"enum of type `{typeof(T).Name}`", static (reader, ordinal) => reader.GetInt16(ordinal));
        return (T)Enum.ToObject(typeof(T), value);
    }

    /// <summary>
    /// Get the nullable enum value from the <see cref="DbDataReader"/>.
    /// </summary>
    /// <param name="dbDataReader"></param>
    /// <param name="columnName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetNullableEnumValueFromInt16<T>(this DbDataReader dbDataReader, string columnName)
        where T : struct, Enum
    {
        if (dbDataReader.IsDBNull(columnName))
        {
            return null;
        }

        var value = dbDataReader.GetInt16Value(columnName);
        return (T)Enum.ToObject(typeof(T), value);
    }

    private static TResult GetRequiredValue<TResult>(this DbDataReader dbDataReader, string columnName, string typeName, Func<DbDataReader, int, TResult> getValue)
    {
        var ordinal = dbDataReader.GetOrdinal(columnName);
        if (dbDataReader.IsDBNull(ordinal))
        {
            throw new InvalidOperationException($"Cannot convert null in column `{columnName}` to {typeName}");
        }

        return getValue(dbDataReader, ordinal);
    }
}