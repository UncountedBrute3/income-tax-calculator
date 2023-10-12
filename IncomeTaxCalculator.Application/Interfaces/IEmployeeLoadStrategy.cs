using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeLoadStrategy
{
    Task<bool> Load(Employee employee);
}