using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Drammer.Common.Infrastructure.EntityFramework;

namespace Drammer.Common.Infrastructure.Tests.EntityFramework;

public sealed class DatabaseFacadeExtensionsTests
{
    [Fact]
    public async Task ExecuteReaderAsync_ShouldNotThrowException()
    {
        // arrange
        const string Sql = "SELECT * FROM Test";
        var dbConnection = new MockDbConnection();

        // act
        var action = () => dbConnection.ExecuteReaderAsync(Sql, _ => new object());

        // assert
        await action.Should().NotThrowAsync<Exception>();
    }

    private class MockDbConnection : DbConnection
    {
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        [AllowNull]
        public override string ConnectionString { get; set; }

        public override string Database { get; }
        public override ConnectionState State => ConnectionState.Open;
        public override string DataSource { get; }
        public override string ServerVersion { get; }

        protected override DbCommand CreateDbCommand()
        {
            return new MockDbCommand();
        }
    }

    private class MockDbCommand : DbCommand
    {
        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object? ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        [AllowNull]
        public override string CommandText { get; set; }

        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }
        protected override DbConnection? DbConnection { get; set; }
        protected override DbParameterCollection DbParameterCollection { get; }
        protected override DbTransaction? DbTransaction { get; set; }
        public override bool DesignTimeVisible { get; set; }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return new Mock<DbDataReader>().Object;
        }
    }
}