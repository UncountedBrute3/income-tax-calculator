using System.Data;
using Dapper;
using IncomeTaxCalculator.Domain.Tables;
using IncomeTaxCalculator.Infrastructure.Interfaces;

namespace IncomeTaxCalculator.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContext _dbContext;
    
    public EmployeeRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Employee employee)
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.ExecuteAsync(AddQuery, new
        {
            employeeId = employee.EmployeeId,
            firstName = employee.FirstName,
            lastName = employee.LastName,
            birthDate = employee.BirthDate.ToDateTime(TimeOnly.MinValue),
            annualIncome = employee.AnnualIncome
        });
    }

    private const string AddQuery = @"
INSERT INTO Employee(EmployeeID, FirstName, LastName, BirthDate, AnnualIncome)
VALUES(@employeeId, @firstName, @lastName, @birthDate, @annualIncome) 
";
}