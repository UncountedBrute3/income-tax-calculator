namespace IncomeTaxCalculator.Application.Interfaces;

public interface IExtractDto
{
    bool IsSuccess { get; }
    int TotalInput { get; set; }
    List<int>? Succeeded { get; set; }
    List<int>? Failed { get; set; }
}