using IncomeTaxCalculator.Domain.Interfaces;

namespace IncomeTaxCalculator.Domain.Tables;

public class TaxBand : ITaxBand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int StartRange { get; set; }
    public int? EndRange { get; set; }
    public int TaxRate { get; set; }
}