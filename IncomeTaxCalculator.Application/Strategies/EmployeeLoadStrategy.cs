using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Domain.Interfaces;
using IncomeTaxCalculator.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Application.Strategies;

public class EmployeeLoadStrategy : IEmployeeLoadStrategy
{
    private readonly IEmployeeRepository _repository;
    private readonly ILogger<EmployeeLoadStrategy> _logger;
    
    public EmployeeLoadStrategy(IEmployeeRepository repository, ILogger<EmployeeLoadStrategy> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Load(IEmployee employee)
    {
        int rowsChanged = await _repository.Add(employee);
        
        bool isCorrectQuantityOfChanges = rowsChanged == 1;
        if (isCorrectQuantityOfChanges)
        {
            _logger.LogError("Unexpected quantity of {rowCount} row(s) changed for employee ID {employeeId}. Expected one.", rowsChanged, employee.EmployeeId);
        }

        return isCorrectQuantityOfChanges;
    }
}