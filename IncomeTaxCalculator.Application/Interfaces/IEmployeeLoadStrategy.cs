using IncomeTaxCalculator.Domain.Interfaces;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeLoadStrategy
{
    Task<bool> Load(IEmployee employee);
}