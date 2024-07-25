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
                //await LoadPackageAsync(context, loggerFactory);

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
                var Orders = JsonConvert.DeserializeObject<List<OrderEntity>>(jsonContent);

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

        //public static async Task LoadPackageAsync(ILoggerFactory loggerFactory, PackageServiceDbContext contex)
        //{
        //    var jsonFilePath = "SeedData/Package.json";
        //    var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        //    var package = JsonConvert.DeserializeObject<List<PackageEntity>>(jsonContent);
        //    if (!await contex.Packages.AnyAsync()) { 
            
        //        for (int i=0; i<package.Count; i++)
        //        {

        //        }
        //    }
        //}
    }

}
