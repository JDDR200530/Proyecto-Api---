using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Proyecto_Poo.Constanst;
using Proyecto_Poo.Database.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Poo.Database.Contex
{
    public class PackageServiceSeeder
    {
        public static async Task LoadDataAsync(
            PackageServiceDbContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            try
            {
                // Cargar roles y usuarios
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);

                // Cargar otras entidades (Orders, Packages, Routes, etc.)
                await LoadEntitiesAsync(context, loggerFactory);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        private static async Task LoadRolesAndUsersAsync(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                // Crear roles si no existen
                await CreateRolesIfNeededAsync(roleManager);

                // Crear usuarios si no existen
                await CreateUsersIfNeededAsync(userManager, loggerFactory);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al cargar roles y usuarios");
            }
        }

        private static async Task CreateRolesIfNeededAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { RolesConstant.ADMIN, RolesConstant.USER };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task CreateUsersIfNeededAsync(UserManager<UserEntity> userManager, ILoggerFactory loggerFactory)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var users = new[]
                {
            new UserEntity { FirstName = "Administrador", LastName = "Paquete", Email = "admin@packageservice.com", UserName = "admin@packageservice.com" },
            new UserEntity { FirstName = "User", LastName = "Paquete", Email = "user@packageservice.com", UserName = "user@packageservice.com" }
        };

                foreach (var user in users)
                {
                    if (await userManager.FindByEmailAsync(user.Email) == null)
                    {
                        var password = "Temporal01!";  // Asegúrate de que esta contraseña cumpla las políticas de seguridad

                        var result = await userManager.CreateAsync(user, password);
                        if (result.Succeeded)
                        {
                            var role = user.UserName.Contains("admin") ? RolesConstant.ADMIN : RolesConstant.USER;
                            await userManager.AddToRoleAsync(user, role);
                        }
                        else
                        {
                            // Loggear cada error específico
                            foreach (var error in result.Errors)
                            {
                                LogError(loggerFactory, new Exception(error.Description), $"Error creando usuario {user.UserName}");
                            }
                        }
                    }
                    else
                    {
                        LogError(loggerFactory, new Exception($"Email ya registrado: {user.Email}"), "Error creando usuario");
                    }
                }
            }
        }


        // Métodos refactorizados para cargar entidades desde archivos JSON
        private static async Task LoadEntitiesAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            await LoadEntityAsync<OrderEntity>(context, loggerFactory, "SeedData/Order.json", context.Orders, entity => entity.OrderDate = DateTime.Now);
            await LoadEntityAsync<PackageEntity>(context, loggerFactory, "SeedData/Package.json", context.Packages);
            await LoadEntityAsync<RouteEntity>(context, loggerFactory, "SeedData/Routes.json", context.Routes);
            await LoadEntityAsync<StopPointEntity>(context, loggerFactory, "SeedData/StopPoint.json", context.StopPoints);
            await LoadEntityAsync<CustomerEntity>(context, loggerFactory, "SeedData/Customer.json", context.Customers);
        }

        // Método genérico para cargar una entidad desde un archivo JSON
        private static async Task LoadEntityAsync<TEntity>(
            PackageServiceDbContext context,
            ILoggerFactory loggerFactory,
            string filePath,
            DbSet<TEntity> dbSet,
            Action<TEntity> additionalSetup = null) where TEntity : class
        {
            try
            {
                // Verificación de existencia de archivo
                if (!File.Exists(filePath))
                {
                    LogError(loggerFactory, new FileNotFoundException($"El archivo {filePath} no fue encontrado."), $"Error al leer archivo {filePath}");
                    return;
                }

                var jsonContent = await File.ReadAllTextAsync(filePath);
                var entities = JsonConvert.DeserializeObject<List<TEntity>>(jsonContent) ?? new List<TEntity>();

                // Verificar si la tabla ya tiene datos
                if (!await dbSet.AnyAsync())
                {
                    foreach (var entity in entities)
                    {
                        additionalSetup?.Invoke(entity);  // Ejecutar configuración adicional si es proporcionada (ej. OrderDate para órdenes)
                    }

                    await dbSet.AddRangeAsync(entities);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                LogError(loggerFactory, e, $"Error seeding {typeof(TEntity).Name}");
            }
        }

        // Método centralizado para loguear errores
        private static void LogError(ILoggerFactory loggerFactory, Exception exception, string message)
        {
            var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
            logger.LogError(exception, message);
        }
    }
}
