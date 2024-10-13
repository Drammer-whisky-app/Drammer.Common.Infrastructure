namespace Drammer.Common.Infrastructure.Tests.Linq;

internal class AsyncEnumeratorWrapper<T> : IAsyncEnumerator<T>
{
    //// taken from: https://stackoverflow.com/questions/48743165/toarrayasync-throws-the-source-iqueryable-doesnt-implement-iasyncenumerable

    private readonly IEnumerator<T> Source;

    public AsyncEnumeratorWrapper(IEnumerator<T> source)
    {
        Source = source;
    }

    public T Current => Source.Current;

    public ValueTask DisposeAsync()
    {
        return new ValueTask(Task.CompletedTask);
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(Source.MoveNext());
    }
}