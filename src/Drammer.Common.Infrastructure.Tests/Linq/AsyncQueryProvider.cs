using System.Linq.Expressions;

namespace Drammer.Common.Infrastructure.Tests.Linq;

internal class AsyncQueryProvider<T> : IQueryProvider
{
    //// taken from: https://stackoverflow.com/questions/48743165/toarrayasync-throws-the-source-iqueryable-doesnt-implement-iasyncenumerable

    private readonly IQueryProvider Source;

    public AsyncQueryProvider(IQueryProvider source)
    {
        Source = source;
    }

    public IQueryable CreateQuery(Expression expression) =>
        Source.CreateQuery(expression);

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
        new AsyncQueryable<TElement>(Source.CreateQuery<TElement>(expression));

    public object Execute(Expression expression) => Execute<T>(expression);

    public TResult Execute<TResult>(Expression expression) =>
        Source.Execute<TResult>(expression);
}