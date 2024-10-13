namespace Drammer.Common.Infrastructure.Cqrs;

public sealed class InvalidSqlQueryException : Exception
{
    public InvalidSqlQueryException(string message, string qeuery)
        : base(message)
    {
        Query = qeuery;
    }

    public string Query { get; }
}