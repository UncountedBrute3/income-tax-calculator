using IncomeTaxCalculator.Application.Models;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IProcessingService
{
    Task<ExtractDto?> Process(Stream data);
}