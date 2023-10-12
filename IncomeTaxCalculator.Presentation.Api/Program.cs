using System.Data;
using IncomeTaxCalculator.Application.Configuration;
using IncomeTaxCalculator.Application.Options;
using IncomeTaxCalculator.Infrastructure.Configurations;
using IncomeTaxCalculator.Infrastructure.Contexts;
using IncomeTaxCalculator.Infrastructure.Interfaces;
using IncomeTaxCalculator.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

DbOptions dbOptions = new();
builder.Configuration.GetSection(DbOptions.Databases).Bind(dbOptions);

TaxBandOptions taxBandOptions = new();
builder.Configuration.GetSection(TaxBandOptions.TaxBand).Bind(taxBandOptions);

// Add services to the container.
builder.Services.AddSingleton(taxBandOptions);
builder.Services.AddSingleton(dbOptions);

builder.Services.AddSingleton<IDbContext, HrDbContext>();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

builder.Services.AddHrMigration(dbOptions);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    scope.ServiceProvider.UpdateHrDatabase();
}

app.Run();