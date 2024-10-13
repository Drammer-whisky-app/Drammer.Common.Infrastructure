using System.Data;
using Drammer.Common.Infrastructure.Cqrs;
using Drammer.Common.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Drammer.Common.Infrastructure.Tests.Cqrs;

public sealed class TemplateCommandHandlerTypedTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExecuteAsync_WithValidQueryTemplate_ExecutesQuery(bool connectionShouldBeDisposed)
    {
        // arrange
        var connectionProvider = new Mock<IConnectionProvider>();
        connectionProvider.Setup(x => x.ShouldBeDisposed).Returns(connectionShouldBeDisposed);

        var command = new TestCommandWithResult();
        var commandHandler = new TestTemplateCommandHandler(
            connectionProvider.Object,
            GenerateValidSql,
            NullLogger<TestTemplateCommandHandler>.Instance);

        // act
        var result = await commandHandler.ExecuteAsync(command);

        // assert
        result.Should().Be(TestTemplateCommandHandler.ExpectedResult);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyQueryTemplate_ThrowsException()
    {
        // arrange
        var connectionProvider = new Mock<IConnectionProvider>();

        var command = new TestCommandWithResult();
        var commandHandler = new TestTemplateCommandHandler(
            connectionProvider.Object,
            _ => string.Empty,
            NullLogger<TestTemplateCommandHandler>.Instance);

        // act
        var action = () => commandHandler.ExecuteAsync(command);

        // assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    private static string GenerateValidSql(TestCommandWithResult command)
    {
        return "-- BEGIN HEADER\n-- END HEADER\nSELECT 1";
    }

    public sealed class TestTemplateCommandHandler : TemplateCommandHandler<TestCommandWithResult, string>
    {
        internal const string ExpectedResult = "test";

        public TestTemplateCommandHandler(
            IConnectionProvider connectionProvider,
            Func<TestCommandWithResult, string> generateSql,
            ILogger<TestTemplateCommandHandler> logger) : base(connectionProvider, generateSql, logger)
        {
        }

        protected override Task<string> ExecuteAsync(
            IDbConnection connection,
            string sql,
            TestCommandWithResult command,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ExpectedResult);
        }
    }
}