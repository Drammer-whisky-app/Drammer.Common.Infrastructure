namespace Drammer.Common.Infrastructure.EntityFramework;

/// <summary>
/// The CreatedModifiedEntity interface.
/// </summary>
public interface ICreatedModifiedEntity
{
    /// <summary>
    ///     Gets or sets the date entity was created.
    /// </summary>
    DateTime? Created { get; set; }

    /// <summary>
    ///     Gets or sets the date entity was last modified.
    /// </summary>
    DateTime? Modified { get; set; }
}