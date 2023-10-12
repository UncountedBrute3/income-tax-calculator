using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Application.Services;
using IncomeTaxCalculator.Domain.Tables;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace IncomeTaxCalculator.Application.Tests.Unit.Services.Tests;

public class ProcessingServiceTests
{
    [Fact]
    public async Task Process_WhenLoaded_ReturnsSuccessfulExtractDto()
    {
        // Arrange.
        using MemoryStream memoryStream = new();
        EmployeeExtract extractItem = new()
        {
            EmployeeId = 1
        };
        Employee transformedItem = new()
        {
            EmployeeId = 1
        };
        
        IEmployeeExtractionStrategy extractor = Substitute.For<IEmployeeExtractionStrategy>();
        extractor.Extract(memoryStream).Returns(new List<EmployeeExtract>()
        {
            extractItem
        }.ToAsyncEnumerable());
        
        IEmployeeTransformStrategy transformer = Substitute.For<IEmployeeTransformStrategy>();
        transformer.Transform(extractItem).Returns(transformedItem);
        
        IEmployeeLoadStrategy loader = Substitute.For<IEmployeeLoadStrategy>();
        loader.Load(transformedItem).Returns(true);
        
        ILogger<ProcessingService> logger = Substitute.For<ILogger<ProcessingService>>();
        
        ProcessingService sut = new(extractor, transformer, loader, logger);

        // Act.
        IExtractDto? actual = await sut.Process(memoryStream);

        // Assert.
        Assert.NotNull(actual);
        Assert.Equal(1, actual.TotalInput);
        Assert.NotNull(actual.Succeeded);
        Assert.Single(actual.Succeeded);
        Assert.NotNull(actual.Failed);
        Assert.Empty(actual.Failed);
    }
    [Fact]
    public async Task Process_WhenLoaded_ReturnsFailedExtractDto()
    {
        // Arrange.
        using MemoryStream memoryStream = new();
        EmployeeExtract extractItem = new()
        {
            EmployeeId = 1
        };
        Employee transformedItem = new()
        {
            EmployeeId = 1
        };
        
        IEmployeeExtractionStrategy extractor = Substitute.For<IEmployeeExtractionStrategy>();
        extractor.Extract(memoryStream).Returns(new List<EmployeeExtract>()
        {
            extractItem
        }.ToAsyncEnumerable());
        
        IEmployeeTransformStrategy transformer = Substitute.For<IEmployeeTransformStrategy>();
        transformer.Transform(extractItem).Returns(transformedItem);
        
        IEmployeeLoadStrategy loader = Substitute.For<IEmployeeLoadStrategy>();
        loader.Load(transformedItem).Returns(false);
        
        ILogger<ProcessingService> logger = Substitute.For<ILogger<ProcessingService>>();
        
        ProcessingService sut = new(extractor, transformer, loader, logger);

        // Act.
        IExtractDto? actual = await sut.Process(memoryStream);

        // Assert.
        Assert.NotNull(actual);
        Assert.Equal(1, actual.TotalInput);
        Assert.NotNull(actual.Succeeded);
        Assert.Empty(actual.Succeeded);
        Assert.NotNull(actual.Failed);
        Assert.Single(actual.Failed);
    }
}