using Microsoft.AspNetCore.Identity;
using Proyecto_Poo;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

Console.WriteLine("JWT Secret: " + builder.Configuration["JWT:Secret"]);
Console.WriteLine("JWT ValidAudience: " + builder.Configuration["JWT:ValidAudience"]);
Console.WriteLine("JWT ValidIssuer: " + builder.Configuration["JWT:ValidIssuer"]);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<PackageServiceDbContext>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await PackageServiceSeeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);
        Console.WriteLine("Seed de datos ejecutado correctamente.");
    }
    catch (Exception seederException)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(seederException, "Error al ejecutar el Seed de Datos");
        Console.WriteLine($"Error al ejecutar el Seed de Datos: {seederException.Message}");
    }
}

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error crítico al iniciar la aplicación: {ex.Message}");
    throw;
}
