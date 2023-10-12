using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace IncomeTaxCalculator.Presentation.Api.Controllers;

[ApiController]
[Route("api/extract")]
public class ExtractController : ControllerBase
{
    private readonly ILogger<ExtractController> _logger;
    private readonly IProcessingService _processingService;

    public ExtractController(IProcessingService processingService, ILogger<ExtractController> logger)
    {
        _processingService = processingService;
        _logger = logger;
    }

    /// <summary>
    /// Method to extract employee data from a CSV file.
    /// </summary>
    /// <param name="file">The file to extract.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Extract(IFormFile file)
    {
        ExtractDto? result = null;
        using (MemoryStream memoryStream = new())
        {
            await file.CopyToAsync(memoryStream);
            result = await _processingService.Process(memoryStream);
        }

        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest();
    }
}