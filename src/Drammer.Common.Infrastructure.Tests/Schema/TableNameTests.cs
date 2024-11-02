using System.ComponentModel.DataAnnotations.Schema;
using Drammer.Common.Infrastructure.Schema;

namespace Drammer.Common.Infrastructure.Tests.Schema;

public sealed class TableNameTests
{
    [Fact]
    public void Get_ReturnsTableName()
    {
        // arrange
        var type = typeof(TestEntity);

        // act
        var result = TableName.Get(type);

        // assert
        result.Should().Be("test-table");
    }

    [Fact]
    public void Get_NoAttribute_ThrowsException()
    {
        // arrange
        var type = typeof(TestEntityNoAttribute);

        // act
        var action = () => TableName.Get(type);

        // assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Table("test-table")]
    private sealed class TestEntity;

    private sealed class TestEntityNoAttribute;
}