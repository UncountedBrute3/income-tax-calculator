using IncomeTaxCalculator.Application.Interfaces;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IProcessingService
{
    Task<IExtractDto?> Process(Stream data);
}