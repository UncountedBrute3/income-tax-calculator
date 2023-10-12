using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Domain.Tables;
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

    public async Task<ExtractDto?> Process(Stream data)
    {
        IEnumerable<EmployeeExtract> extractedEmployees = _extractor.Extract(data);
        ExtractDto result = new(); 
        foreach (EmployeeExtract extract in extractedEmployees)
        {
            result.TotalInput++;
            
            try
            {
                Employee transformedEmployee = _transformer.Transform(extract);
                bool loaded = await _loader.Load(transformedEmployee);
                if (loaded)
                {
                    result.Succeeded.Add(extract.EmployeeID);
                }
                else
                {
                    result.Failed.Add(extract.EmployeeID);
                }
            }
            catch (Exception exception)
            {
                result.Failed.Add(extract.EmployeeID);
                _logger.LogError(exception, "Unexpected error occured.");
            }
        }

        return result;
    }
}