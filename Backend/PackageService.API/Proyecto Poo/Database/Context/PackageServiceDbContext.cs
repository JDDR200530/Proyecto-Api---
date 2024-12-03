using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Proyecto_Poo.Database.Configuration;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Service;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Database.Contex
{
    public class PackageServiceDbContext : IdentityDbContext<UserEntity>
    {
        private readonly IAudtiService _audtiService;

        public PackageServiceDbContext(DbContextOptions<PackageServiceDbContext> options, IAudtiService audtiService)
            : base(options)
        {
            _audtiService = audtiService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");


            modelBuilder.HasDefaultSchema("security");


           
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");


     
            modelBuilder.ApplyConfiguration(new OrderCofiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        
            var eTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in eTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            modelBuilder.Entity<PackageEntity>()
            .HasOne(a => a.Order)
            .WithMany(tp => tp.Packages)
            .HasForeignKey(a => a.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TruckEntity>()
        .HasMany(t => t.Orders) 
        .WithOne(o => o.Truck)  
        .HasForeignKey(o => o.TruckId) 
        .OnDelete(DeleteBehavior.Restrict); 

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));
            var userId = _audtiService.GetUserId();

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = userId;
                        entity.CreatedDate = DateTime.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedBy = userId;
                        entity.UpdatedDate = DateTime.Now;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<PaymentEntity> Total { get; set; }
        public DbSet<ShipmentEntity> Pay { get; set; }

        public DbSet<TruckEntity> Trucks { get; set; }

    }
}

