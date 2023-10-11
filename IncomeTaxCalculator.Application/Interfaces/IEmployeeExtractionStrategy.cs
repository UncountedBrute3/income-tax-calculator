namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeExtractionStrategy
{
    IAsyncEnumerable<IEmployeeExtract> Extract(Stream data);
}