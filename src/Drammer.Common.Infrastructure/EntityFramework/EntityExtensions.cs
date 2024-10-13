namespace Drammer.Common.Infrastructure.EntityFramework;

/// <summary>
/// The entity extensions.
/// </summary>
public static class EntityExtensions
{
    /// <summary>
    /// Returns true when the entity key has a null or default value.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <returns>A bool.</returns>
    public static bool IsIdentifierDbNull<TKey>(this Entity<TKey> entity)
    {
        var isNullOrDefault = entity.Id == null || entity.Id.Equals(default(TKey));
        if (isNullOrDefault)
        {
            return isNullOrDefault;
        }

        if (entity.Id is int id)
        {
            return id == 0;
        }

        return false;
    }
}