using FluentMigrator;

namespace IncomeTaxCalculator.Infrastructure.Migrations;

[Migration(20231011134100)]
public class HrVersionOneMigration : Migration
{
    public override void Up()
    {
        Create.Table("Employee")
            .WithColumn("EmployeeID").AsInt32().PrimaryKey().NotNullable()
            .WithColumn("FirstName").AsAnsiString(50).Nullable()
            .WithColumn("LastName").AsAnsiString(50).Nullable()
            .WithColumn("BirthDate").AsDate().Nullable()
            .WithColumn("AnnualIncome").AsDecimal(10, 2).Nullable();
    }

    public override void Down()
    {
        Delete.Table("Employee");
    }
}