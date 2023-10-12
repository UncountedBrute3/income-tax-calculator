using System.Data;
using IncomeTaxCalculator.Infrastructure.Contexts;
using IncomeTaxCalculator.Infrastructure.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace IncomeTaxCalculator.Infrastructure.Tests.Unit.Contexts.Tests;

public class HrDbContextTests
{
    [Fact]
    public void CreateConnection_ReturnsSqliteConnection()
    {
        // Arrange.
        HrDbContext sut = new(new DbOptions()
        {
            HrConnectionString = "Data Source=:memory:"
        });

        // Act and Assert.
        Assert.IsType<SqliteConnection>(sut.CreateConnection());
    }
}