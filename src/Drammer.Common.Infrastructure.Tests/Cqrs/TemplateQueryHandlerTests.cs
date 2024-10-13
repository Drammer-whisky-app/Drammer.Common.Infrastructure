using System.Data;
using Drammer.Common.Infrastructure.Cqrs;
using Drammer.Common.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Drammer.Common.Infrastructure.Tests.Cqrs;

public sealed class TemplateQueryHandlerTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExecuteAsync_WithValidQueryTemplate_ExecutesQuery(bool connectionShouldBeDisposed)
    {
        // arrange
        var connectionProvider = new Mock<IConnectionProvider>();
        connectionProvider.Setup(x => x.ShouldBeDisposed).Returns(connectionShouldBeDisposed);

        var query = new TestQuery();
        var queryHandler = new TestTemplateQueryHandler(
            connectionProvider.Object,
            GenerateValidSql,
            NullLogger<TestTemplateQueryHandler>.Instance);

        // act
        var result = await queryHandler.ExecuteAsync(query);

        // assert
        result.Should().Be(TestTemplateQueryHandler.ExpectedResult);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyQueryTemplate_ThrowsException()
    {
        // arrange
        var connectionProvider = new Mock<IConnectionProvider>();

        var query = new TestQuery();
        var queryHandler = new TestTemplateQueryHandler(
            connectionProvider.Object,
            _ => string.Empty,
            NullLogger<TestTemplateQueryHandler>.Instance);

        // act
        var action = () => queryHandler.ExecuteAsync(query);

        // assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    private static string GenerateValidSql(TestQuery query)
    {
        return "-- BEGIN HEADER\n-- END HEADER\nSELECT 1";
    }

    public sealed class TestTemplateQueryHandler : TemplateQueryHandler<TestQuery, int>
    {
        internal const int ExpectedResult = 10;

        public TestTemplateQueryHandler(
            IConnectionProvider connectionProvider,
            Func<TestQuery, string> generateSql,
            ILogger<TestTemplateQueryHandler> logger) : base(connectionProvider, generateSql, logger)
        {
        }

        protected override Task<int> ExecuteAsync(IDbConnection connection, string sql, TestQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ExpectedResult);
        }
    }
}