using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Drammer.Common.Infrastructure.Schema;

/// <summary>
/// The table name functions.
/// </summary>
public static class TableName
{
    /// <summary>
    /// Returns the name of the table.
    /// </summary>
    /// <param name="type">The entity type.</param>
    /// <returns>A <see cref="string"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the <see cref="TableAttribute"/> is not found.</exception>
    public static string Get(Type type)
    {
        var attributes = type.GetCustomAttributes(typeof(TableAttribute)).ToList();

        if (attributes == null || attributes.Count == 0)
        {
            throw new InvalidOperationException($"Type {type.Name} does not have a TableAttribute");
        }

        return ((attributes.First() as TableAttribute)!).Name;
    }
}