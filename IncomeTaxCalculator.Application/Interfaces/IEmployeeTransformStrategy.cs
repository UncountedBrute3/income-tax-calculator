using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeTransformStrategy
{
    Employee Transform(EmployeeExtract extract);
}