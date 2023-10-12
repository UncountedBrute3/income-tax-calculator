using IncomeTaxCalculator.Application.Models;

namespace IncomeTaxCalculator.Application.Tests.Unit.Models.Tests;

public class ExtractDtoTests
{
    [Fact]
    public void Constructor_ReturnsExpectedDefaults()
    {
        ExtractDto actual = new();
        Assert.True(actual.IsSuccess);
        Assert.Equal(default, actual.TotalInput);
        Assert.Empty(actual.Succeeded);
        Assert.Empty(actual.Failed);
    }

    [Theory]
    [InlineData(10, 10, 0, true)]
    [InlineData(10, 9, 1, false)]
    [InlineData(10, 5, 0, false)]
    [InlineData(10, 0, 10, false)]
    public void IsSuccess_WhenPassedValues_ReturnsExpectedBoolean(int total, int toSucceed, int toFail, bool expected)
    {
        // Arrange.
        ExtractDto actual = new()
        {
            TotalInput = total,
            Succeeded = Enumerable.Range(1, toSucceed).ToList(),
            Failed = Enumerable.Range(1, toFail).ToList()
        };

        // Assert.
        Assert.Equal(expected,actual.IsSuccess);
    }
}