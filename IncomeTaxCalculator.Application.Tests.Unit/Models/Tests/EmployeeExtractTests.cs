using IncomeTaxCalculator.Application.Models;

namespace IncomeTaxCalculator.Application.Tests.Unit.Models.Tests;

public class EmployeeExtractTests
{
    [Fact]
    public void Constructor_ReturnsExpectedDefaults()
    {
        EmployeeExtract actual = new();
        Assert.Equal(default, actual.EmployeeID);
        Assert.Equal("", actual.FirstName);
        Assert.Equal("", actual.LastName);
        Assert.Equal(default, actual.DateOfBirth);
        Assert.Equal(default, actual.GrossAnnualSalary);
    }
}