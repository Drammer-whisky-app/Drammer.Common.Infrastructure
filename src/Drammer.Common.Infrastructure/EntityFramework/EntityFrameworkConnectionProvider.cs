using System.Data;
using System.Diagnostics.CodeAnalysis;
using Drammer.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Drammer.Common.Infrastructure.EntityFramework;

/// <summary>
/// The Entity Framework connection provider.
/// </summary>
public sealed class EntityFrameworkConnectionProvider : IConnectionProvider
{
    private readonly DbContext _context;

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="context"></param>
    public EntityFrameworkConnectionProvider(DbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public bool ShouldBeDisposed => false;

    /// <summary>
    /// Gets the underlying DbConnection. Should not be disposed.
    /// </summary>
    /// <returns>An <see cref="IDbConnection"/>.</returns>
    [ExcludeFromCodeCoverage(Justification = "Cannot be mocked")]
    public IDbConnection GetDbConnection()
    {
        return _context.Database.GetDbConnection();
    }
}