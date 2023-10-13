# Income Tax Calculator

This solution aims to show how to calculate net income from an employee dataset which includes the gross income.

## Running Application

Running the application via Rider or Visual Studio in debug mode will construct as "hrdb.db" file in the project directory.
The database already exists in the repository so you can inspect the outcome before you run your own version.
You will need to delete the records out of the database or delete the database file entirely to run the process with the
same data again.

You can also run the unit and integration tests to verify the expected outcome.

## Packages Used

### Implementation

| Package                      | Reason                                                                      |
|------------------------------|-----------------------------------------------------------------------------|
| CsvHelper                    | Provides an easy way to read CSV information.                               |
| FluentMigrator               | Provides a way to migrate a database structure to multiple environments.    |
| FluentMigrator.Runner        | ""                                                                          |
| FluentMigrator.Runner.SQLite | The SQLite extension that enables FluentMigrator to work with the provider. |
| Swashbuckle.AspNetCore       | Bundles as part of ASP.NET 7 Template, however used for Swagger support.    |
| Dapper                       | Provides an efficient ORM for database changes.                             |

### Unit Testing

| Package     | Reason                            |
|-------------|-----------------------------------|
| xUnit       | Unit Testing framework of choice. |
| NSubstitute | Mocking framework of choice.      |

### Load Testing

| Package | Reason                                          |
|---------|-------------------------------------------------|
| xUnit   | Unit Testing framework of choice.               |
| Dapper  | Provides an efficient ORM for database changes. |

## Known Areas for Improvement

1. CSVHelper is currently being used to return an `IEnumerable`. I ran into issues with `IAsyncEnumerable` with stream
   issues so backed out. Converting to `IAsyncEnumerable` would yield better memory efficiency.
2. Load tests would highlight any issues the ETL process could face if a large quantity of changes were sent to the API.