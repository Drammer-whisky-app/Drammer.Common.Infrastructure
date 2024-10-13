using Drammer.Common.Infrastructure.Cqrs;

namespace Drammer.Common.Infrastructure.Tests.Cqrs;

public sealed class QueryTemplateHelperTests
{
    [Fact]
    public void RemoveQueryHeader_ValidQuery_ReturnsQueryWithoutHeader()
    {
        // arrange
        var query = $"-- BEGIN HEADER\n" +
                    $"TEST\n" +
                    $"-- END HEADER\n" +
                    $"THE QUERY\n";

        // act
        var result = QueryTemplateHelper.RemoveQueryHeader(query);

        // assert
        Assert.NotNull(result);
        Assert.Equal("THE QUERY", result);
    }

    [Fact]
    public void RemoveQueryHeader_ValidQuery2_ReturnsQueryWithoutHeader()
    {
        // arrange
        var query = $"-- START\n" +
                    $"-- BEGIN HEADER\n" +
                    $"TEST\n" +
                    $"-- END HEADER\n" +
                    $"THE QUERY\n";

        // act
        var result = QueryTemplateHelper.RemoveQueryHeader(query);

        // assert
        Assert.NotNull(result);
        Assert.Equal("-- START\n\nTHE QUERY", result);
    }

    [Fact]
    public void RemoveQueryHeader_ValidEmptyQuery_ReturnsQueryWithoutHeader()
    {
        // arrange
        var query = $"-- BEGIN HEADER\n" +
                    $"TEST\n" +
                    $"-- END HEADER\n";

        // act
        var result = QueryTemplateHelper.RemoveQueryHeader(query);

        // assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData("QUERY")]
    [InlineData("--BEGIN HEADER\nQUERY")]
    [InlineData("--END HEADER\nQUERY")]
    public void RemoveQueryHeader_QueryWithoutHeader_ThrowsException(string query)
    {
        // act
        Assert.Throws<InvalidSqlQueryException>(() => { _ = QueryTemplateHelper.RemoveQueryHeader(query); });
    }
}