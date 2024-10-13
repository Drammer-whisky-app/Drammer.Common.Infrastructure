using Drammer.Common.Infrastructure.EntityFramework;

namespace Drammer.Common.Infrastructure.Tests.EntityFramework;

public sealed class EntityExtensionsTests
{
    [Theory]
    [InlineData(1, false)]
    [InlineData(0, true)]
    [InlineData(-1, false)]
    public void IsIdentifierDbNull_GivenInt(int value, bool isNull)
    {
        // arrange
        var entity = new IntEntity { Id = value, };

        // act
        var result = entity.IsIdentifierDbNull();

        // assert
        Assert.Equal(isNull, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, true)]
    [InlineData(null, true)]
    [InlineData(-1, false)]
    public void IsIdentifierDbNull_GivenNullableInt(int? value, bool isNull)
    {
        // arrange
        var entity = new NullableIntEntity { Id = value, };

        // act
        var result = entity.IsIdentifierDbNull();

        // assert
        Assert.Equal(isNull, result);
    }

    private sealed class IntEntity : Entity<int>
    {
    }

    private sealed class NullableIntEntity : Entity<int?>
    {
    }
}