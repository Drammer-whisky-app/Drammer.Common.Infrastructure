using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Infrastructure.QueryStorage;

public static class QueryStorageServiceCollectionExtensions
{
    public static IServiceCollection AddEmbeddedResourceQueryStorage(this IServiceCollection services, Type type)
    {
        services.AddSingleton<IQueryStorage>(_ => new EmbeddedResourceQueryStorage(type));
        return services;
    }
}