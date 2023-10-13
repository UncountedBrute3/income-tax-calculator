using System.Data;
using System.Net;
using System.Text.Json;
using Dapper;
using IncomeTaxCalculator.Application.Models;
using IncomeTaxCalculator.Domain.Tables;
using IncomeTaxCalculator.Infrastructure.Configurations;
using IncomeTaxCalculator.Infrastructure.Contexts;
using IncomeTaxCalculator.Infrastructure.Options;
using IncomeTaxCalculator.Presentation.Api.Tests.Integration.Configuration;
using IncomeTaxCalculator.Presentation.Api.Tests.Integration.TestData;

namespace IncomeTaxCalculator.Presentation.Api.Tests.Integration.Tests;

public class ExtractTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ExtractTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [ClassData(typeof(ExtractTransformsAndLoadsCorrectlyTestData))]
    public async Task Extract_TransformsAndLoadsCorrectly(string fileName, ExtractDto expectedResponse,
        Employee[] expectedEmployees)
    {
        // Arrange.
        // Scope must be left open during test execution otherwise the DB will be disposed.
        IServiceScope scope = _factory.Services.CreateScope();
        scope.ServiceProvider.UpdateHrDatabase();

        HttpClient client = _factory.CreateClient();

        await using FileStream stream = File.OpenRead(fileName);
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/extract");
        using MultipartFormDataContent content = new MultipartFormDataContent
        {
            { new StreamContent(stream), "file", "data.csv" }
        };

        request.Content = content;

        // Act.
        HttpResponseMessage response = await client.SendAsync(request);

        // Assert.
        await AssertResponse(expectedResponse, response);

        HrDbContext context = new HrDbContext(new DbOptions()
        {
            HrConnectionString = _factory.TestDbConnection
        });
        Employee[] actualEmployees = await GetActualEmployees(context);

        Assert.Equal(expectedEmployees.Length, actualEmployees.Length);
        foreach (Employee actualEmployee in actualEmployees)
        {
            Employee? expectedEmployee = Array.Find(expectedEmployees, e => e.EmployeeId == actualEmployee.EmployeeId);
            AssertEmployee(expectedEmployee, actualEmployee);
        }
    }

    private static void AssertEmployee(Employee? expectedEmployee, Employee actualEmployee)
    {
        Assert.NotNull(expectedEmployee);
        Assert.Equal(expectedEmployee.EmployeeId, actualEmployee.EmployeeId);
        Assert.Equal(expectedEmployee.FirstName, actualEmployee.FirstName);
        Assert.Equal(expectedEmployee.LastName, actualEmployee.LastName);
        Assert.Equal(expectedEmployee.BirthDate, actualEmployee.BirthDate);
        Assert.Equal(expectedEmployee.AnnualIncome, actualEmployee.AnnualIncome);
    }

    private static async Task<Employee[]> GetActualEmployees(HrDbContext context)
    {
        Employee[] actualEmployees;
        using (IDbConnection connection = context.CreateConnection())
        {
            IEnumerable<dynamic> result = await connection.QueryAsync(AllEmployees);
            actualEmployees = result.Select(r => new Employee()
            {
                EmployeeId = (int)r.EmployeeID,
                FirstName = r.FirstName,
                LastName = r.LastName,
                BirthDate = DateOnly.FromDateTime(DateTime.Parse(r.BirthDate)),
                AnnualIncome = (decimal)r.AnnualIncome
            }).ToArray();
        }

        return actualEmployees;
    }

    private static async Task AssertResponse(ExtractDto expectedResponse, HttpResponseMessage response)
    {
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

        await using Stream jsonString = await response.Content.ReadAsStreamAsync();
        ExtractDto? actual = await JsonSerializer.DeserializeAsync<ExtractDto>(jsonString);
        Assert.NotNull(actual);
        Assert.Equal(expectedResponse.IsSuccess, actual.IsSuccess);
        Assert.Equal(expectedResponse.TotalInput, actual.TotalInput);
        Assert.Equal(expectedResponse.Succeeded.Count, actual.Succeeded.Count);
        Assert.Equal(expectedResponse.Failed.Count, actual.Failed.Count);
    }

    private const string AllEmployees = @"
SELECT 
    EmployeeID,
    FirstName,
    LastName,
    BirthDate,
    AnnualIncome
FROM Employee
";
}