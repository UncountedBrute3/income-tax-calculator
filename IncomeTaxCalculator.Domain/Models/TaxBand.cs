namespace IncomeTaxCalculator.Domain.Models;

public class TaxBand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int StartRange { get; set; }
    public int EndRange { get; set; } = int.MaxValue;
    public int TaxRate { get; set; }

    public decimal TaxPercentage => (decimal)TaxRate / 100;
}