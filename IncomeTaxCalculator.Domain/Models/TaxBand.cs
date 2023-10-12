namespace IncomeTaxCalculator.Domain.Models;

/// <summary>
/// Class to represent a tax band that can be used to calculate net income.
/// </summary>
public class TaxBand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int StartRange { get; set; }
    
    // If no end range provided, default to max.
    public int EndRange { get; set; } = int.MaxValue;
    public int TaxRate { get; set; }

    /// <summary>
    /// Returns the <see cref="TaxRate"/> converted to decimal.
    /// </summary>
    public decimal TaxPercentage => (decimal)TaxRate / 100;
}