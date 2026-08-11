using ECommerce.API;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding;
using ECommerce.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();
builder.Services.AddApplication();

// Add services to the container.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var scope = app.Services.CreateAsyncScope();

    var dbSeed = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();

    await dbContext.Database.MigrateAsync();
    await dbSeed.SeedAll();
}

app.UseExceptionHandler();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

