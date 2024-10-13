using System.Data;
using System.Diagnostics.CodeAnalysis;
using Drammer.Common.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Drammer.Common.Infrastructure.EntityFramework;

public sealed class EntityFrameworkConnectionProvider : IConnectionProvider
{
    private readonly DbContext _context;

    public EntityFrameworkConnectionProvider(DbContext context)
    {
        _context = context;
    }

    public bool ShouldBeDisposed => false;

    /// <summary>
    /// Gets the underlying DbConnection. Should not be disposed.
    /// </summary>
    /// <returns></returns>
    [ExcludeFromCodeCoverage(Justification = "Cannot be mocked")]
    public IDbConnection GetDbConnection()
    {
        return _context.Database.GetDbConnection();
    }
}