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

        // If employee database is outside of our control,
        // this would go into a different database that is configurable.
        Create.Table("TaxBands")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("StartRange").AsInt32().NotNullable()
            .WithColumn("EndRange").AsInt32().NotNullable();
        
        Insert.IntoTable("TaxBands")
            .Row(new { Name = "Tax Band A", StartRange = "superadmin@mvcapp.com",PASSWORD_HASH = "dfgkmdglkdmfg34532+"});
    }

    public override void Down()
    {
        Delete.Table("Employee");
        Delete.Table("TaxBands");
    }
}