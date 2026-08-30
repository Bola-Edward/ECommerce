using ECommerce.API;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding;
using ECommerce.UseCases;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPresentation();
    builder.Services.AddApplication();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbSeed = scope.ServiceProvider
            .GetRequiredService<DatabaseSeeder>();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ECommerceDbContext>();

        await dbContext.Database.MigrateAsync();
        await dbSeed.SeedAll();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}