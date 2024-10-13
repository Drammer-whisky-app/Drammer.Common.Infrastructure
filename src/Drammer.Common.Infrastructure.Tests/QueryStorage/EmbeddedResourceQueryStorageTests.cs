using Drammer.Common.Infrastructure.QueryStorage;

namespace Drammer.Common.Infrastructure.Tests.QueryStorage;

public sealed class EmbeddedResourceQueryStorageTests
{
    [Fact]
    public async Task GetQuery_ValidQuery_ReturnsQuery()
    {
        // arrange
        var storage = new EmbeddedResourceQueryStorage(GetType());

        // act
        var query = await storage.GetQueryAsync("Query1.sql");

        // assert
        Assert.NotNull(query);
        Assert.Contains("SELECT", query);
    }

    [Fact]
    public async Task GetQuery_WithoutSqlExtensionValidQuery_ReturnsQuery()
    {
        // arrange
        var storage = new EmbeddedResourceQueryStorage(GetType());

        // act
        var query = await storage.GetQueryAsync("Query1");

        // assert
        Assert.NotNull(query);
        Assert.Contains("SELECT", query);
    }

    [Fact]
    public async Task GetQuery_ValidQueryCacheEnabled_ReturnsQuery()
    {
        // arrange
        var storage = new EmbeddedResourceQueryStorage(GetType());
        var tasks = new List<Task>();
        for (var i = 0; i < 10; i++)
        {
            tasks.Add(storage.GetQueryAsync("Query1.sql"));
        }

        // act
        var query1 = await storage.GetQueryAsync("Query1.sql");
        await Task.WhenAll(tasks);
        var query2 = await storage.GetQueryAsync("Query1.sql");

        // assert
        Assert.NotNull(query1);
        Assert.Equal(query1, query2);
    }
}