using IncomeTaxCalculator.Application.Models;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeExtractionStrategy
{
    IEnumerable<EmployeeExtract> Extract(Stream data);
}