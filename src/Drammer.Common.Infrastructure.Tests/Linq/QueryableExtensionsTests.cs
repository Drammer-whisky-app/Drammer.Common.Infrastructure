using Drammer.Common.Infrastructure.Linq;
using Drammer.Common.Paging;

namespace Drammer.Common.Infrastructure.Tests.Linq;

public sealed class QueryableExtensionsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ToPagedList_ReturnsPagedList()
    {
        // arrange
        var query = _fixture.CreateMany<TestEntity>(5).AsQueryable();
        var pageIndex = 1;
        var pageSize = 2;

        // act
        var result = query.ToPagedList(pageIndex, pageSize);

        // assert
        result.Should().BeOfType<PagedList<TestEntity>>();
        result.PageIndex.Should().Be(pageIndex);
        result.PageSize.Should().Be(pageSize);
        result.Should().HaveCount(pageSize);
    }

    [Fact]
    public void ToPagedList_WithSelector_ReturnsPagedList()
    {
        // arrange
        var query = _fixture.CreateMany<TestEntity>(5).AsQueryable();
        var pageIndex = 1;
        var pageSize = 2;

        // act
        var result = query.ToPagedList(pageIndex, pageSize, x => x);

        // assert
        result.Should().BeOfType<PagedList<TestEntity>>();
        result.PageIndex.Should().Be(pageIndex);
        result.PageSize.Should().Be(pageSize);
        result.Should().HaveCount(pageSize);
    }

    [Fact]
    public async Task ToPagedListAsync_ReturnsPagedList()
    {
        // arrange
        var query = _fixture.CreateMany<TestEntity>(5).AsAsyncQueryable();
        var pageIndex = 1;
        var pageSize = 2;

        // act
        var result = await query.ToPagedListAsync(pageIndex, pageSize);

        // assert
        result.Should().BeOfType<PagedList<TestEntity>>();
        result.PageIndex.Should().Be(pageIndex);
        result.PageSize.Should().Be(pageSize);
        result.Should().HaveCount(pageSize);
    }

    [Fact]
    public async Task ToPagedListAsync_WithSelector_ReturnsPagedList()
    {
        // arrange
        var query = _fixture.CreateMany<TestEntity>(5).AsAsyncQueryable();
        var pageIndex = 1;
        var pageSize = 2;

        // act
        var result = await query.ToPagedListAsync(pageIndex, pageSize, x => x);

        // assert
        result.Should().BeOfType<PagedList<TestEntity>>();
        result.PageIndex.Should().Be(pageIndex);
        result.PageSize.Should().Be(pageSize);
        result.Should().HaveCount(pageSize);
    }

    private sealed class TestEntity
    {
        public int Id { get; set; }
    }
}