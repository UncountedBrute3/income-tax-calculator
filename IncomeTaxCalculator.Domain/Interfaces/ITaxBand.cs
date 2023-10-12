namespace IncomeTaxCalculator.Domain.Interfaces;

public interface ITaxBand
{
    int Id { get; set; }
    string Name { get; set; }
    int StartRange { get; set; }
    int EndRange { get; set; }
    int TaxRate { get; set; }
    decimal TaxPercentage { get; }
}