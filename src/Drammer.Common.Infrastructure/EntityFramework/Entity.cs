using System.ComponentModel.DataAnnotations;
using Drammer.Common.Domain;

namespace Drammer.Common.Infrastructure.EntityFramework;

public abstract class Entity<TKey> : IEntity
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    [Key]
    public TKey? Id { get; set; }
}