namespace Drammer.Common.Infrastructure.EntityFramework;

/// <summary>
/// The created/modified entity.
/// </summary>
/// <typeparam name="TKey">
/// The key type.
/// </typeparam>
public abstract class CreatedModifiedEntity<TKey> : Entity<TKey>, ICreatedModifiedEntity
{
    /// <summary>
    /// Gets or sets the date entity was created.
    /// </summary>
    public virtual DateTime? Created { get; set; }

    /// <summary>
    /// Gets or sets the date entity was last modified.
    /// </summary>
    public virtual DateTime? Modified { get; set; }
}