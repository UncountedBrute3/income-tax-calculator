using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Domain.Interfaces;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeTransformStrategy
{
    IEmployee Transform(IEmployeeExtract extract);
}