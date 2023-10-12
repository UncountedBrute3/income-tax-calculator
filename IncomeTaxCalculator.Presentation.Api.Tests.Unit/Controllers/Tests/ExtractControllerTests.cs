using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Presentation.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace IncomeTaxCalculator.Presentation.Api.Tests.Unit.Controllers.Tests;

public class ExtractControllerTests
{
    [Fact]
    public async Task Extract_WhenFileValid_ReturnsOkWithIExtractDto()
    {
        // Arrange.
        ILogger<ExtractController> logger = Substitute.For<ILogger<ExtractController>>();
        IProcessingService processingService = Substitute.For<IProcessingService>();
        processingService.Process(Arg.Any<MemoryStream>()).Returns(new ExtractDto()
        {
            TotalInput = 1,
            Succeeded = new List<int>() { 2 }
        });
        string content = "Hello World from a Fake File";
        string fileName = "test.csv";
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        ExtractController sut = new(processingService, logger);

        // Act.
        IActionResult actual = await sut.Extract(file);
        OkObjectResult? actualResult = actual as OkObjectResult;

        // Assert.
        Assert.NotNull(actualResult);
        Assert.Equal(200, actualResult.StatusCode);
        ExtractDto actualDto = Assert.IsType<ExtractDto>(actualResult.Value);
        Assert.True(actualDto.IsSuccess);
        Assert.Equal(1, actualDto.TotalInput);
        Assert.Single(actualDto.Succeeded);
        Assert.Empty(actualDto.Failed);
    }

    [Fact]
    public async Task Extract_WhenNullResultReturned_ReturnsBadRequest()
    {
        // Arrange.
        ILogger<ExtractController> logger = Substitute.For<ILogger<ExtractController>>();
        IProcessingService processingService = Substitute.For<IProcessingService>();
        processingService.Process(Arg.Any<MemoryStream>()).ReturnsNull();
        string content = "Hello World from a Fake File";
        string fileName = "test.csv";
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        ExtractController sut = new(processingService, logger);

        // Act.
        IActionResult actual = await sut.Extract(file);
        BadRequestResult? actualResult = actual as BadRequestResult;

        // Assert.
        Assert.NotNull(actualResult);
        Assert.Equal(400, actualResult.StatusCode);
    }

    [Fact]
    public async Task Extract_WhenExceptionOccurs_Returns500()
    {
        // Arrange.
        ILogger<ExtractController> logger = Substitute.For<ILogger<ExtractController>>();
        IProcessingService processingService = Substitute.For<IProcessingService>();
        processingService.Process(Arg.Any<MemoryStream>()).Throws(new Exception("Unexpected error occured."));
        string content = "Hello World from a Fake File";
        string fileName = "test.csv";
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        ExtractController sut = new(processingService, logger);

        // Act.
        Exception actual = await Assert.ThrowsAsync<Exception>(() => sut.Extract(file));

        // Assert.
        Assert.Contains("Unexpected error occured.", actual.Message);
    }
}