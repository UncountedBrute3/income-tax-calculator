using System.Data;
using IncomeTaxCalculator.Infrastructure.Contexts;
using IncomeTaxCalculator.Infrastructure.Options;
using Microsoft.Data.SqlClient;

namespace IncomeTaxCalculator.Infrastructure.Tests.Unit.Contexts.Tests;

public class HrDbContextTests
{
    [Fact]
    public void CreateConnection_ReturnsSqlConnection()
    {
        // Arrange.
        HrDbContext sut = new(new DbOptions()
        {
            HrConnectionString = ":memory;Version=3;New=true;"
        });

        // Act and Assert.
        Assert.IsType<SqlConnection>(sut.CreateConnection());
    }
}