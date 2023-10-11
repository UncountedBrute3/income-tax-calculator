using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Application.Services;

public class ProcessingService : IProcessingService
{
    private readonly IEmployeeExtractionStrategy _extractor;
    private readonly IEmployeeTransformStrategy _transformer;
    private readonly IEmployeeLoadStrategy _loader;
    private readonly ILogger<ProcessingService> _logger;

    public ProcessingService(IEmployeeExtractionStrategy extractor, IEmployeeTransformStrategy transformer,
        IEmployeeLoadStrategy loader, ILogger<ProcessingService> logger)
    {
        _extractor = extractor;
        _transformer = transformer;
        _loader = loader;
        _logger = logger;
    }

    public async Task<IExtractDto?> Process(Stream data)
    {
        IAsyncEnumerable<IEmployeeExtract> extractedEmployees = _extractor.Extract(data);
        ExtractDto result = new();
        await foreach (IEmployeeExtract extract in extractedEmployees)
        {
            result.TotalInput++;
            
            try
            {
                IEmployee transformedEmployee = _transformer.Transform(extract);
                bool loaded = await _loader.Load(transformedEmployee);
                if (loaded)
                {
                    result.Succeeded.Add(extract.EmployeeId);
                }
                else
                {
                    result.Failed.Add(extract.EmployeeId);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unexpected error occured.");
            }
        }

        return result;
    }
}