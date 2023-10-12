namespace IncomeTaxCalculator.Domain.Tables;

/// <summary>
/// Class to represent an Employee in the target repository.
/// </summary>
public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly BirthDate { get; set; }
    public decimal AnnualIncome { get; set; }
}