using IncomeTaxCalculator.Domain.Interfaces;

namespace IncomeTaxCalculator.Domain.Tables;

public class Employee : IEmployee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly BirthDate { get; set; }
    public decimal AnnualIncome { get; set; }
}