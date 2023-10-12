using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Domain.Tests.Unit.Tables.Tests;

public class EmployeeTests
{
    [Fact]
    public void Constructor_ReturnsExpectedDefault()
    {
        Employee actual = new();
        
        Assert.Equal(default, actual.EmployeeId);
        Assert.Equal("", actual.FirstName);
        Assert.Equal("", actual.LastName);
        Assert.Equal(default, actual.BirthDate);
        Assert.Equal(default, actual.AnnualIncome);
    }
}