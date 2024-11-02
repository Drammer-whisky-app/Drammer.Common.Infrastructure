using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Infrastructure.EntityFramework;

/// <summary>
/// The service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the created modified interceptor.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCreatedModifiedInterceptor(this IServiceCollection services)
    {
        services.AddSingleton<CreatedModifiedInterceptor>();
        return services;
    }
}