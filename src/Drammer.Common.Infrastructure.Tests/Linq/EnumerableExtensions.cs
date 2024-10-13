namespace Drammer.Common.Infrastructure.Tests.Linq;

public static class EnumerableExtensions
{
    public static IQueryable<T> AsAsyncQueryable<T>(this ICollection<T> source) =>
        new AsyncQueryable<T>(source.AsQueryable());

    public static IQueryable<T> AsAsyncQueryable<T>(this IEnumerable<T> source) =>
        new AsyncQueryable<T>(source.AsQueryable());
}