using System.Collections;
using System.Linq.Expressions;

namespace Drammer.Common.Infrastructure.Tests.Linq;

internal class AsyncQueryable<T> : IAsyncEnumerable<T>, IQueryable<T>
{
    //// taken from: https://stackoverflow.com/questions/48743165/toarrayasync-throws-the-source-iqueryable-doesnt-implement-iasyncenumerable

    private IQueryable<T> Source;

    public AsyncQueryable(IQueryable<T> source)
    {
        Source = source;
    }

    public Type ElementType => typeof(T);

    public Expression Expression => Source.Expression;

    public IQueryProvider Provider => new AsyncQueryProvider<T>(Source.Provider);

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new AsyncEnumeratorWrapper<T>(Source.GetEnumerator());
    }

    public IEnumerator<T> GetEnumerator() => Source.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}