using System.Data;
using Dapper;
using IncomeTaxCalculator.Domain.Tables;
using IncomeTaxCalculator.Infrastructure.Interfaces;

namespace IncomeTaxCalculator.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContext _dbContext;
    
    public EmployeeRepository(IDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Method to add an employee to the database.
    /// </summary>
    /// <param name="employee">The employee to add.</param>
    /// <returns>An <see cref="int"/> indicating the number of rows changed.</returns>
    public async Task<int> Add(Employee employee)
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.ExecuteAsync(AddQuery, new
        {
            employeeId = employee.EmployeeId,
            firstName = employee.FirstName,
            lastName = employee.LastName,
            // Due to limitations of SQLite and Dapper, only date time is supported.
            birthDate = employee.BirthDate.ToDateTime(TimeOnly.MinValue),
            annualIncome = employee.AnnualIncome
        });
    }

    private const string AddQuery = @"
INSERT INTO Employee(EmployeeID, FirstName, LastName, BirthDate, AnnualIncome)
VALUES(@employeeId, @firstName, @lastName, @birthDate, @annualIncome) 
";
}