using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Application.Options;
using IncomeTaxCalculator.Domain.Models;
using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Application.Strategies;

/// <summary>
/// Class to handle transforming the employee data into the target model.
/// </summary>
public class EmployeeTransformStrategy : IEmployeeTransformStrategy
{
    private readonly TaxBand[] _taxBands;

    public EmployeeTransformStrategy(TaxBandOptions taxBandsOptions)
    {
        if (taxBandsOptions.TaxBands == null)
        {
            throw new ArgumentNullException(nameof(taxBandsOptions.TaxBands),"Tax band options is incorrectly configured.");
        }
        
        // To ensure we process taxes in the correct order.
        _taxBands = taxBandsOptions.TaxBands
            .OrderBy(o => o.StartRange)
            .ThenBy(o => o.EndRange)
            .ToArray();
    }
    
    /// <summary>
    /// Method to transform the <see cref="EmployeeExtract"/> into an <see cref="Employee"/>.
    /// </summary>
    /// <param name="extract">The extract to convert.</param>
    /// <returns>A converted employee.</returns>
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

    /// <summary>
    /// Method to calculate the net income from the gross income.
    /// </summary>
    /// <param name="grossIncome">The gross income to calculate.</param>
    /// <returns>The gross income.</returns>
    private decimal CalculateNetIncome(decimal grossIncome)
    {
        decimal totalTax = 0;
        foreach (TaxBand taxBand in _taxBands)
        {
            // Income is less than the bands start range.
            // Therefore, no changes needed.
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