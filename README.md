# Income Tax Calculator

This solution aims to show how to calculate net income from an employee dataset which includes the gross income.

## Packages Used

### Implementation

| Package | Reason |
| ------- | ------ |
| CsvHelper | Provides an easy way to read CSV information.
| FluentMigrator | Provides a way to migrate a database structure to multiple environments. |
| FluentMigrator.Runner | "" |
| FluentMigrator.Runner.SQLite | The SQLite extension that enables FluentMigrator to work with the provider. |
| Swashbuckle.AspNetCore | Bundles as part of ASP.NET 7 Template, however used for Swagger support. |

### Unit Testing

| Package | Reason |
| ------- | ------ |
| xUnit | Unit Testing framework of choice. |
| NSubstitute | Mocking framework of choice. |

## Known Areas for Improvement

1. CSVHelper is currently being used to return an `IEnumerable`. I ran into issues with `IAsyncEnumerable` with stream issues so backed out. Converting to `IAsyncEnumerable` would yield better memory efficiency.
2. Integration tests would identify more issues that unit tests can't. They would take some time to write and set up.
3. Load tests would highlight any issues the ETL process could face if a large quantity of changes were sent to the API.