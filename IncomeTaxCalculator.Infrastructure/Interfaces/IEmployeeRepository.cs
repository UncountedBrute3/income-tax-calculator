using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Infrastructure.Interfaces;

public interface IEmployeeRepository
{
    Task<int> Add(Employee employee);
}