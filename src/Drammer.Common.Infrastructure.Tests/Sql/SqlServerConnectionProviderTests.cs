using Drammer.Common.Infrastructure.Sql;

namespace Drammer.Common.Infrastructure.Tests.Sql;

public sealed class SqlServerConnectionProviderTests
{
    [Fact]
    public void GetConnection_ReturnsConnection()
    {
        // arrange
        const string FakeConnectionString =
            "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        var provider = new SqlServerConnectionProvider(FakeConnectionString);

        // act
        var connection = provider.GetDbConnection();

        // assert
        connection.Should().NotBeNull();
        provider.ShouldBeDisposed.Should().Be(true);
    }
}