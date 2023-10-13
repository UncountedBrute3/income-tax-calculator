using System.Collections;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Domain.Tables;

namespace IncomeTaxCalculator.Presentation.Api.Tests.Integration.TestData;

public class ExtractTransformsAndLoadsCorrectlyTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "./Assets/TestOne.csv",
            new ExtractDto()
            {
                Succeeded = new()
                {
                    101,
                    102,
                    103,
                    104,
                    105
                },
                Failed = new(),
                TotalInput = 5
            },
            new Employee[]
            {
                new()
                {
                    EmployeeId = 101,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = new DateOnly(1980, 05, 15),
                    AnnualIncome = 41000
                },
                new()
                {
                    EmployeeId = 102,
                    FirstName = "Jane",
                    LastName = "Smith",
                    BirthDate = new DateOnly(1990, 08, 22),
                    AnnualIncome = 50000
                },
                new()
                {
                    EmployeeId = 103,
                    FirstName = "Michael",
                    LastName = "Johnson",
                    BirthDate = new DateOnly(1975, 03, 10),
                    AnnualIncome = 59000
                },
                new()
                {
                    EmployeeId = 104,
                    FirstName = "Emily",
                    LastName = "Williams",
                    BirthDate = new DateOnly(1988, 11, 05),
                    AnnualIncome = 53000
                },
                new()
                {
                    EmployeeId = 105,
                    FirstName = "David",
                    LastName = "Brown",
                    BirthDate = new DateOnly(1995, 07, 18),
                    AnnualIncome = 38000
                }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}