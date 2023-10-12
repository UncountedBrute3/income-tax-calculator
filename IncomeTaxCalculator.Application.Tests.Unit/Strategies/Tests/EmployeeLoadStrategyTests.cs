using IncomeTaxCalculator.Application.Strategies;
using IncomeTaxCalculator.Domain.Tables;
using IncomeTaxCalculator.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace IncomeTaxCalculator.Application.Tests.Unit.Strategies.Tests;

public class EmployeeLoadStrategyTests
{
    [Fact]
    public async Task Load_WhenEmployeePassedAndRepoReturnsTrue_ReturnsTrue()
    {
        // Arrange.
        Employee employee = new()
        {
            EmployeeId = 1
        };

        IEmployeeRepository employeeRepository = Substitute.For<IEmployeeRepository>();
        employeeRepository.Add(employee).Returns(1);
        ILogger<EmployeeLoadStrategy> logger = Substitute.For<ILogger<EmployeeLoadStrategy>>();

        EmployeeLoadStrategy sut = new(employeeRepository, logger);

        // Act.
        bool actual = await sut.Load(employee);

        // Assert.
        Assert.True(actual);
        logger.DidNotReceiveWithAnyArgs().LogError("test", "test");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    public async Task Load_WhenEmployeePassedAndRepoReturnsFalse_ReturnsFalseAndLogs(int rowsChanged)
    {
        // Arrange.
        Employee employee = new()
        {
            EmployeeId = 1
        };

        IEmployeeRepository employeeRepository = Substitute.For<IEmployeeRepository>();
        employeeRepository.Add(employee).Returns(rowsChanged);
        ILogger<EmployeeLoadStrategy> logger = Substitute.For<ILogger<EmployeeLoadStrategy>>();

        EmployeeLoadStrategy sut = new(employeeRepository, logger);

        // Act.
        bool actual = await sut.Load(employee);

        // Assert.
        Assert.False(actual);
        logger.ReceivedWithAnyArgs().LogError("test", "test");
    }
}