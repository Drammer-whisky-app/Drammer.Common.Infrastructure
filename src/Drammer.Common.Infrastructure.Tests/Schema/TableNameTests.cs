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

    [Table("test-table")]
    private sealed class TestEntity;
}