using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Domain.Interfaces;
using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Application.Strategies;

public class EmployeeTransformStrategy : IEmployeeTransformStrategy
{
    private readonly ITaxBand[] _taxBands;

    public EmployeeTransformStrategy(ITaxBand[] taxBands)
    {
        _taxBands = taxBands
            .OrderBy(o => o.StartRange)
            .ThenBy(o => o.EndRange)
            .ToArray();
    }
    
    public IEmployee Transform(IEmployeeExtract extract)
    {
        Employee employee = new Employee()
        {
            EmployeeId = extract.EmployeeId,
            FirstName = extract.FirstName,
            LastName = extract.LastName,
            BirthDate = extract.DateOfBirth,
            AnnualIncome = CalculateNetIncome(extract.GrossAnnualSalary)
        };

        return employee;
    }

    private decimal CalculateNetIncome(decimal grossIncome)
    {
        decimal totalTax = 0;
        foreach (ITaxBand taxBand in _taxBands)
        {
            if (grossIncome <= taxBand.StartRange)
            {
                break;
            }

            decimal taxableAtThisRate = Math.Min(taxBand.EndRange - taxBand.StartRange, grossIncome - taxBand.StartRange);
            decimal taxThisBand = taxableAtThisRate * taxBand.TaxPercentage;
            totalTax += taxThisBand;
        }

        return grossIncome - totalTax;
    }
}