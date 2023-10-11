using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Application.Strategies;
using IncomeTaxCalculator.Domain.Interfaces;
using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Application.Tests.Unit.Strategies.Tests;

public class EmployeeTransformStrategyTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(4500, 4500)]
    [InlineData(5000, 5000)]
    [InlineData(5001, 5000.2)]
    [InlineData(9999, 8999.2)]
    [InlineData(10000, 9000)]
    [InlineData(40000, 29000)]
    public void Transform_WhenIEmployeeExtractPassed_ReturnsCorrectIEmployee(decimal gross, decimal net)
    {
        // Arrange.
        EmployeeExtract extract = new()
        {
            EmployeeId = 1,
            FirstName = "Test",
            LastName = "Name",
            DateOfBirth = new DateOnly(1992, 05, 16),
            GrossAnnualSalary = gross
        };
        Employee expected = new()
        {
            EmployeeId = 1,
            FirstName = "Test",
            LastName = "Name",
            BirthDate = new DateOnly(1992, 05, 16),
            AnnualIncome = net
        };
        TaxBand[] taxBands = new[]
        {
            new TaxBand()
            {
                StartRange = 0,
                EndRange = 5000,
                TaxRate = 0
            },
            new TaxBand()
            {
                StartRange = 5000,
                EndRange = 20000,
                TaxRate = 20
            },
            new TaxBand()
            {
                StartRange = 20000,
                EndRange = -1,
                TaxRate = 40
            },
        };
        EmployeeTransformStrategy sut = new(taxBands);

        // Act.
        IEmployee actual = sut.Transform(extract);

        // Assert.
        Assert.Equal(expected.EmployeeId, actual.EmployeeId);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.BirthDate, actual.BirthDate);
        Assert.Equal(expected.AnnualIncome, actual.AnnualIncome);
    }
}