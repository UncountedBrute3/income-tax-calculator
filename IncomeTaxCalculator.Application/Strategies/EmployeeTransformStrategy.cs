using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Application.Options;
using IncomeTaxCalculator.Domain.Models;
using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Application.Strategies;

public class EmployeeTransformStrategy : IEmployeeTransformStrategy
{
    private readonly TaxBand[] _taxBands;

    public EmployeeTransformStrategy(TaxBandOptions taxBandsOptions)
    {
        if (taxBandsOptions.TaxBands == null)
        {
            throw new ArgumentNullException(nameof(taxBandsOptions.TaxBands),"Tax band options is incorrectly configured.");
        }
        
        _taxBands = taxBandsOptions.TaxBands
            .OrderBy(o => o.StartRange)
            .ThenBy(o => o.EndRange)
            .ToArray();
    }
    
    public Employee Transform(EmployeeExtract extract)
    {
        Employee employee = new Employee()
        {
            EmployeeId = extract.EmployeeID,
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
        foreach (TaxBand taxBand in _taxBands)
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