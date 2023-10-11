namespace IncomeTaxCalculator.Application.Interfaces;

public interface IEmployeeExtract
{
    int EmployeeId { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    DateOnly DateOfBirth { get; set; }
    decimal GrossAnnualSalary { get; set; }
}