using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Domain.Tests.Unit.Tables.Tests;

public class TaxBandTests
{
    [Fact]
    public void Constructor_ReturnsExpectedDefault()
    {
        TaxBand actual = new();
        
        Assert.Equal(default, actual.Id);
        Assert.Equal("", actual.Name);
        Assert.Equal(default, actual.StartRange);
        Assert.Equal(default, actual.EndRange);
        Assert.Equal(default, actual.TaxRate);
        Assert.Equal(0, actual.TaxPercentage);
    }
}