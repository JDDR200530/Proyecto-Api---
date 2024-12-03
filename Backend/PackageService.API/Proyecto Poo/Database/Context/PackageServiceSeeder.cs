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
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Proyecto_Poo.Database.Contex
{
    public class PackageServiceSeeder
    {
        public static async Task LoadDataAsync(
            PackageServiceDbContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            try
            {
                await LoadRolesAndUserAsync(userManager, roleManager, loggerFactory);
                await LoadCustomerAsync(loggerFactory, context);
                await LoadOrderAsync(loggerFactory, context);
                await LoadPackageAsync(loggerFactory, context);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error inicializacion la data del API");
            }
        }
        private static async Task LoadRolesAndUserAsync(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
          )
        {
            try
            {
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.USER));
                }
                if (!await userManager.Users.AnyAsync())
                {
                    var userAdmin = new UserEntity
                    {
                        FirstName = "Administrador",
                        LastName = "ServicioPaqueteria",
                        Email = "admin@ServicioPaqueteria.hn",
                        UserName = "admin@ServicioPaqueteria.hn",
                    };

                    var normalUser = new UserEntity
                    {
                        FirstName = "User",
                        LastName = "ServicioPaqueteria",
                        Email = "user@ServicioPaqueteria.hn",
                        UserName = "user@ServicioPaqueteria.hn",
                    };
                    await userManager.CreateAsync(userAdmin, "Temporal01*");
                    await userManager.CreateAsync(normalUser, "Temporal01*");

                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
                    await userManager.AddToRoleAsync(normalUser, RolesConstant.USER);
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e.Message);
            }
        }

        private  static async Task LoadOrderAsync(ILoggerFactory loggerFactory, PackageServiceDbContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Order.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var orders = JsonConvert.DeserializeObject<List<OrderEntity>>(jsonContent);

                if (!await context.Orders.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].CreatedBy = user.Id;
                        orders[i].CreatedDate = DateTime.Now;
                        orders[i].UpdatedBy = user.Id;
                        orders[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(orders);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Orders");
            }
        }
        public static async Task LoadPackageAsync(ILoggerFactory loggerFactory, PackageServiceDbContext context)
        {
            try
            {

                var jsonFilePath = "SeedData/Package.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var Package = JsonConvert.DeserializeObject<List<PackageEntity>>(jsonContent);

                if (!await context.Packages.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < Package.Count; i++) 
                    {
                        Package[i].CreatedBy = user.Id;
                        Package[i].CreatedDate = DateTime.Now;
                        Package[i].UpdatedBy = user.Id;
                        Package[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(Package);
                    await context.SaveChangesAsync();  
                }

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Package");
            }
        }


        public static async Task LoadCustomerAsync(ILoggerFactory loggerFactory, PackageServiceDbContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Customer.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var customer = JsonConvert.DeserializeObject<List<CustomerEntity>>(jsonContent);
                if (!await context.Customers.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < customer.Count; i++)
                    {
                        customer[i].CreatedBy = user.Id;
                        customer[i].CreatedDate = DateTime.Now;
                        customer[i].UpdatedBy = user.Id;
                        customer[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(customer);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Customer");
            }
        }
    }
}
