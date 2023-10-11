namespace IncomeTaxCalculator.Domain.Interfaces;

public interface IEmployee
{
    int EmployeeId { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    DateOnly BirthDate { get; set; }
    decimal AnnualIncome { get; set; }
}