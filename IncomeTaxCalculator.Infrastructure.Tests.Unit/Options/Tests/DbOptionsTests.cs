using IncomeTaxCalculator.Domain.Tables;
using IncomeTaxCalculator.Infrastructure.Options;

namespace IncomeTaxCalculator.Infrastructure.Tests.Unit.Options.Tests;

public class DbOptionsTests
{
    [Fact]
    public void Constructor_ReturnsExpectedDefault()
    {
        DbOptions actual = new();
        
        Assert.Equal("", actual.HrConnectionString);
    }
}