using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using Proyecto_Poo.Database.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Proyecto_Poo.Database.Contex
{
    public class PackageServiceSeeder
    {
        public static async Task LoadDataAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await LoadOrderAsync(context, loggerFactory);
                await LoadPackageAsync(context, loggerFactory);
                await LoadCustomerAsync(context, loggerFactory);
                await LoadRoutesAsync(context, loggerFactory);
                await LoadStopPointAsync(context, loggerFactory);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error inicializando la data del API.");
            }
        }

        public static async Task LoadOrderAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePath = "SeedData/Order.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var Orders = JsonConvert.DeserializeObject<List<OrderEntity>>(jsonContent) ?? new List<OrderEntity>();

                if (!await context.Orders.AnyAsync())
                {
                    foreach (var order in Orders)
                    {
                        order.OrderDate = DateTime.Now;
                    }

                    await context.Orders.AddRangeAsync(Orders);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed Order.");
            }
        }

        public static async Task LoadPackageAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePath = "SeedData/Package.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var Packages = JsonConvert.DeserializeObject<List<PackageEntity>>(jsonContent) ?? new List<PackageEntity>();

                if (!await context.Packages.AnyAsync())
                {
                    await context.Packages.AddRangeAsync(Packages);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed Package.");
            }
        }

        public static async Task LoadRoutesAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePath = "SeedData/Routes.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var routes = JsonConvert.DeserializeObject<List<RouteEntity>>(jsonContent) ?? new List<RouteEntity>();

                foreach (var route in routes)
                {
                    // Validar que RouteName no sea nulo o vacío
                    if (string.IsNullOrWhiteSpace(route.RouteName))
                    {
                        throw new ArgumentException($"RouteName cannot be null or empty for RouteId: {route.RouteId}");
                    }

                    var existingRoute = await context.Routes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(r => r.RouteId == route.RouteId);

                    if (existingRoute == null)
                    {
                        await context.Routes.AddAsync(route);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Routes.");
            }
        }
        public static async Task LoadStopPointAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePath = "SeedData/StopPoint.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var stopPoints = JsonConvert.DeserializeObject<List<StopPointEntity>>(jsonContent) ?? new List<StopPointEntity>();

                foreach (var stopPoint in stopPoints)
                {
                    // Comprueba si el StopPoint ya está en el contexto
                    var existingStopPoint = context.StopPoints.Local
                        .FirstOrDefault(sp => sp.StopPointId == stopPoint.StopPointId);

                    if (existingStopPoint != null)
                    {
                        // Si ya existe en el contexto, desvincúlalo
                        context.Entry(existingStopPoint).State = EntityState.Detached;
                    }

                    // Verifica en la base de datos si existe el StopPoint
                    existingStopPoint = await context.StopPoints
                        .AsNoTracking()
                        .FirstOrDefaultAsync(sp => sp.StopPointId == stopPoint.StopPointId);

                    if (existingStopPoint == null)
                    {
                        // Añade la entidad si no existe
                        await context.StopPoints.AddAsync(stopPoint);
                    }
                    else
                    {
                        // Actualiza la entidad si ya existe
                        context.Entry(existingStopPoint).CurrentValues.SetValues(stopPoint);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de StopPoints.");
            }
        }
        public static async Task LoadCustomerAsync(PackageServiceDbContext context, ILoggerFactory loggerFactory)
            {
                try
                {
                    var jsonFilePath = "SeedData/Customer.json";
                    var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                    var customers = JsonConvert.DeserializeObject<List<CustomerEntity>>(jsonContent) ?? new List<CustomerEntity>();

                    if (!await context.Customers.AnyAsync())
                    {
                        await context.Customers.AddRangeAsync(customers);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                    logger.LogError(e, "Error al ejecutar el Seed de Customer.");
                }
            }
        }
    } 

