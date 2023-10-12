namespace IncomeTaxCalculator.Application.Models;

/// <summary>
/// Class to represent the data coming from the extract.
/// </summary>
public class EmployeeExtract
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public decimal GrossAnnualSalary { get; set; }
}