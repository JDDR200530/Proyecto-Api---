using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Proyecto_Poo.Database.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Proyecto_Poo.Database.Contex
{
    public class PackageServiceSeeder
    {
        public static async Task LoadDataAsync(
           PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await LoadOrderAsync(context, loggerFactory);
                await LoadPackageAsync(context, loggerFactory);

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
                var orders = JsonConvert.DeserializeObject<List<OrderEntity>>(jsonContent);

                if (!await context.Orders.AnyAsync())
                {
                    foreach (var order in orders)
                    {
                        order.OrderDate = DateTime.Now;
                    }

                    await context.Orders.AddRangeAsync(orders);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed Order.");
            }
        }

        public static async Task LoadPackageAsync(
            PackageServiceDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var jsonFilePath = "SeedData/package.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var packages = JsonConvert.DeserializeObject<List<PackageEntity>>(jsonContent);

                

                await context.Packages.AddRangeAsync(packages);
                await context.SaveChangesAsync();
                
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed Packages.");
            }
        }
    }

}
