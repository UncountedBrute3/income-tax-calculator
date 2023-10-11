using IncomeTaxCalculator.Infrastructure.Configurations;
using IncomeTaxCalculator.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

DbOptions dbOptions = new();
builder.Configuration.GetSection(DbOptions.Databases).Bind(dbOptions);

// Add services to the container.
builder.Services.AddSingleton(dbOptions);

builder.Services.AddHrMigration(dbOptions);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();