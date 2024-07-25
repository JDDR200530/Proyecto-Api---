using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Proyecto_Poo.Database.Context
{
    public class PackageServiceDbContext : DbContext
    {
        

        public PackageServiceDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }
    }
}
