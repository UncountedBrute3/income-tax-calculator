using IncomeTaxCalculator.Domain.Interfaces;

namespace IncomeTaxCalculator.Infrastructure.Interfaces;

public interface IEmployeeRepository
{
    Task<int> Add(IEmployee employee);
}