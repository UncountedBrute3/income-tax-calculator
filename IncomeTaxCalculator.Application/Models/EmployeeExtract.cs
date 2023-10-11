using IncomeTaxCalculator.Application.Interfaces;

namespace IncomeTaxCalculator.Application.Models;

public class EmployeeExtract : IEmployeeExtract
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public decimal GrossAnnualSalary { get; set; }
}