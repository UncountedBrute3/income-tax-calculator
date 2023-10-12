using IncomeTaxCalculator.Application.Interfaces;

namespace IncomeTaxCalculator.Application.Models;

public class EmployeeExtract
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public decimal GrossAnnualSalary { get; set; }
}