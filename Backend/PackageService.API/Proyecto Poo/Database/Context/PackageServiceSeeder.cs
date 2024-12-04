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
                await LoadPaymentsAsync(loggerFactory, context);
                await LoadTruckAsync(loggerFactory, context);
                await LoadShipmentAsync(loggerFactory, context);
                

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

        public static async Task LoadPaymentsAsync(ILoggerFactory loggerFactory, PackageServiceDbContext context) 
        {
            try
            {
                var jsonFilePath = "SeedData/Payments.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var payment = JsonConvert.DeserializeObject<List<PaymentEntity>>(jsonContent);

                if (!await context.Payments.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < payment.Count; i++)
                    {
                        payment[i].CreatedBy = user.Id;
                        payment[i].CreatedDate = DateTime.Now;
                        payment[i].UpdatedBy = user.Id;
                        payment[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(payment);
                    await context.SaveChangesAsync();
                }
            }

            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger(typeof(PackageServiceSeeder));
                logger.LogError(e, "Error al ejecutar el Seed Payments");
            }
        }

        public static async Task LoadTruckAsync(ILoggerFactory loggerFactory, PackageServiceDbContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Truck.json";
                var jsonContext = await File.ReadAllTextAsync(jsonFilePath);
                var truck = JsonConvert.DeserializeObject<List<TruckEntity>>(jsonContext);

                if (!await context.Trucks.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < truck.Count; i++)
                    {
                        truck[i].CreatedBy = user.Id;
                        truck[i].CreatedDate = DateTime.Now;
                        truck[i].UpdatedBy = user.Id;
                        truck[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(truck);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecucion el Seeed de Trucks");
            }; 
        }

        public static async Task LoadShipmentAsync(ILoggerFactory loggerFactory, PackageServiceDbContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Shipments.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var shipments = JsonConvert.DeserializeObject<List<ShipmentEntity>>(jsonContent);
                if (!await context.Shipments.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < shipments.Count; i++)
                    {
                        shipments[i].CreatedBy = user.Id;
                        shipments[i].CreatedDate = DateTime.Now;
                        shipments[i].UpdatedBy = user.Id;
                        shipments[i].UpdatedDate = DateTime.Now;
                    }
                    context.AddRange(shipments);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e) 
            {
                var logger= loggerFactory.CreateLogger<PackageServiceSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Shipments");
            }
        }


    }
}
