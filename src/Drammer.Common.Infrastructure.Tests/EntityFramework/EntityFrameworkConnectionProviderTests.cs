using Drammer.Common.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Drammer.Common.Infrastructure.Tests.EntityFramework;

public sealed class EntityFrameworkConnectionProviderTests
{
    [Fact]
    public void Constructor_ShouldBeDisposed_ReturnsFalse()
    {
        // arrange
        var context = new Mock<DbContext>();

        // act
        var provider = new EntityFrameworkConnectionProvider(context.Object);

        // assert
        provider.ShouldBeDisposed.Should().Be(false);
    }
}