using System.Data;
using Dapper;
using IncomeTaxCalculator.Domain.Interfaces;
using IncomeTaxCalculator.Infrastructure.Interfaces;

namespace IncomeTaxCalculator.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContext _dbContext;
    
    public EmployeeRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(IEmployee employee)
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.ExecuteAsync(AddQuery, employee);
    }

    private const string AddQuery = @"
INSERT INTO Employee(EmployeeID, FirstName, LastName, BirthDate, AnnualIncome)
VALUES(@employeeId, @firstName, @lastName, @birthDate, @annualIncome) 
";
}