namespace IncomeTaxCalculator.Infrastructure.Options;

public class DbOptions
{
    public const string Databases = "Databases";
    public string HrConnectionString { get; set; } = "";
}