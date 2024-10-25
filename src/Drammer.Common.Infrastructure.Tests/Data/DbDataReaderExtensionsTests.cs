using System.Data.Common;
using DbDataReaderExtensions = Drammer.Common.Infrastructure.Data.DbDataReaderExtensions;

namespace Drammer.Common.Infrastructure.Tests.Data;

public sealed class DbDataReaderExtensionsTests
{
    private const int DefaultColumnIndex = 0;

    private readonly Fixture _fixture = new();

    [Fact]
    public void GetString_ReturnsString()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<string>();
        reader.Setup(x => x.GetString(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetString(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNEnum_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNEnum<TestEnum>(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNEnum_WhenColumnIsNotNull_ReturnsEnum()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetInt32(DefaultColumnIndex)).Returns((int)TestEnum.Value2);

        // act
        var result = DbDataReaderExtensions.GetNEnum<TestEnum>(reader.Object, column);

        // assert
        result.Should().Be(TestEnum.Value2);
    }

    private Mock<DbDataReader> CreateDbDataReaderMock(out string column)
    {
        var columnName = _fixture.Create<string>();
        var reader = new Mock<DbDataReader>();
        reader.Setup(x => x.GetOrdinal(columnName)).Returns(DefaultColumnIndex);

        column = columnName;
        return reader;
    }

    private enum TestEnum
    {
        Value1,
        Value2
    }
}