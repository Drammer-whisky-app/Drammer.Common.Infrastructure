using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Drammer.Common.Infrastructure.Cqrs;

namespace Drammer.Common.Infrastructure.QueryStorage;

public sealed class EmbeddedResourceQueryStorage : IQueryStorage
{
    private const string SqlExtension = ".sql";
    private readonly Type _assemblyType;
    private Dictionary<string, string>? _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddedResourceQueryStorage"/> class.
    /// </summary>
    /// <param name="assemblyType">An object representing a type in the assembly where the SQL scripts are stored.</param>
    public EmbeddedResourceQueryStorage(Type assemblyType)
    {
        _assemblyType = assemblyType ?? throw new ArgumentNullException(nameof(assemblyType));
    }

    /// <inheritdoc />
    public async Task<string> GetQueryAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (_cache == null)
        {
            await LoadFilesAsync(cancellationToken).ConfigureAwait(false);
        }

        var queryName = name.ToLower();
        if (!queryName.EndsWith(SqlExtension, StringComparison.OrdinalIgnoreCase))
        {
            queryName += SqlExtension;
        }

        if (!_cache.TryGetValue(queryName, out var cachedValue))
        {
            throw new SqlQueryNotFoundException(queryName);
        }

        return cachedValue;
    }

    private static string GetFilename(string resourceName)
    {
        var splitted = resourceName.Split(".");
        return $"{splitted[^2]}.{splitted[^1]}";
    }

    [MemberNotNull(nameof(_cache))]
    private async Task LoadFilesAsync(CancellationToken cancellationToken = default)
    {
        _cache = new Dictionary<string, string>();

        var assembly = Assembly.GetAssembly(_assemblyType);
        if (assembly == null)
        {
            throw new InvalidOperationException($"An assembly of type {assembly} is not found.");
        }

        var resources = assembly.GetManifestResourceNames();
        foreach (var r in resources.Where(x => x.EndsWith(SqlExtension, StringComparison.OrdinalIgnoreCase)))
        {
            await using var stream = assembly.GetManifestResourceStream(r);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException("stream cannot be null"));
            var result = QueryTemplateHelper.RemoveQueryHeader(await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false));

            if (!string.IsNullOrWhiteSpace(result))
            {
                _cache.Add(GetFilename(r).ToLower(), result);
            }
        }
    }
}