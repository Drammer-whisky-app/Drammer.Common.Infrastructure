using System.Data.Common;
using DbDataReaderExtensions = Drammer.Common.Infrastructure.Data.DbDataReaderExtensions;

namespace Drammer.Common.Infrastructure.Tests.Data;

public sealed class DbDataReaderExtensionsTests
{
    private const int DefaultColumnIndex = 0;

    private readonly Fixture _fixture = new();

    [Fact]
    public void GetStringValue_ReturnsString()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<string>();
        reader.Setup(x => x.GetString(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetStringValue(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableStringValue_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableStringValue(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableStringValue_WhenColumnIsNotNull_ReturnsString()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<string>();
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetString(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetNullableStringValue(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetInt32Value_ReturnsInt32()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<int>();
        reader.Setup(x => x.GetInt32(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetInt32Value(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableInt32Value_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableInt32Value(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableInt32Value_WhenColumnIsNotNull_ReturnsInt32()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<int>();
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetInt32(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetNullableInt32Value(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetInt64Value_ReturnsInt64()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<long>();
        reader.Setup(x => x.GetInt64(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetInt64Value(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableInt64Value_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableInt64Value(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableInt64Value_WhenColumnIsNotNull_ReturnsInt64()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<long>();
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetInt64(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetNullableInt64Value(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetDecimalValue_ReturnsDecimal()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<decimal>();
        reader.Setup(x => x.GetDecimal(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetDecimalValue(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableDecimalValue_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableDecimalValue(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableDecimalValue_WhenColumnIsNotNull_ReturnsDecimal()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<decimal>();
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetDecimal(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetNullableDecimalValue(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetBooleanValue_ReturnsBoolean()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<bool>();
        reader.Setup(x => x.GetBoolean(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetBooleanValue(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableBooleanValue_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableBooleanValue(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableBooleanValue_WhenColumnIsNotNull_ReturnsBoolean()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<bool>();
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetBoolean(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetNullableBooleanValue(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetDateTimeValueAsUtc_ReturnsDateTime()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<DateTime>().ToUniversalTime();
        reader.Setup(x => x.GetDateTime(DefaultColumnIndex)).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetDateTimeValueAsUtc(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableDateTimeValueAsUtc_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableDateTimeValueAsUtc(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableDateTimeValueAsUtc_WhenColumnIsNotNull_ReturnsDateTime()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = _fixture.Create<DateTime>().ToUniversalTime();
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.SetupGet(x => x[column]).Returns(value);

        // act
        var result = DbDataReaderExtensions.GetNullableDateTimeValueAsUtc(reader.Object, column);

        // assert
        result.Should().Be(value.ToUniversalTime());
    }

    [Fact]
    public void GetEnumValue_ReturnsEnum()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        var value = TestEnum.Value1;
        reader.Setup(x => x.GetInt32(DefaultColumnIndex)).Returns((int)value);

        // act
        var result = DbDataReaderExtensions.GetEnumValue<TestEnum>(reader.Object, column);

        // assert
        result.Should().Be(value);
    }

    [Fact]
    public void GetNullableEnumValue_WhenColumnIsNull_ReturnsNull()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(true);

        // act
        var result = DbDataReaderExtensions.GetNullableEnumValue<TestEnum>(reader.Object, column);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetNullableEnumValue_WhenColumnIsNotNull_ReturnsEnum()
    {
        // arrange
        var reader = CreateDbDataReaderMock(out var column);
        reader.Setup(x => x.IsDBNull(DefaultColumnIndex)).Returns(false);
        reader.Setup(x => x.GetInt32(DefaultColumnIndex)).Returns((int)TestEnum.Value2);

        // act
        var result = DbDataReaderExtensions.GetNullableEnumValue<TestEnum>(reader.Object, column);

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