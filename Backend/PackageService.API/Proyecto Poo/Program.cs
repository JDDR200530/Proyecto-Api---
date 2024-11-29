using Microsoft.AspNetCore.Identity;
using Proyecto_Poo;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;

var builder = WebApplication.CreateBuilder(args);


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
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al ejecutar el Seed de Datos");
    }
}

app.Run();