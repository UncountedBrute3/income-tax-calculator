﻿using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Domain.Tables;
using IncomeTaxCalculator.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace IncomeTaxCalculator.Application.Strategies;

/// <summary>
/// Class to handle loading of the <see cref="Employee"/> data into the repository.
/// </summary>
public class EmployeeLoadStrategy : IEmployeeLoadStrategy
{
    private readonly IEmployeeRepository _repository;
    private readonly ILogger<EmployeeLoadStrategy> _logger;
    
    public EmployeeLoadStrategy(IEmployeeRepository repository, ILogger<EmployeeLoadStrategy> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Method to load the <see cref="Employee"/> data into the repository.
    /// </summary>
    /// <param name="employee">The employee to add.</param>
    /// <returns>True if only 1 row was changed. Otherwise, False.</returns>
    public async Task<bool> Load(Employee employee)
    {
        int rowsChanged = await _repository.Add(employee);
        
        bool isCorrectQuantityOfChanges = rowsChanged == 1;
        if (!isCorrectQuantityOfChanges)
        {
            _logger.LogError("Unexpected quantity of {rowCount} row(s) changed for employee ID {employeeId}. Expected one.", rowsChanged, employee.EmployeeId);
        }

        return isCorrectQuantityOfChanges;
    }
}