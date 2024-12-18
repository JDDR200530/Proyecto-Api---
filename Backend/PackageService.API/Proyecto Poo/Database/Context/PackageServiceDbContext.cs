﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new TruckConfiguration());

        
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
        
            modelBuilder.Entity<ShipmentEntity>()
            .HasOne(s => s.Order)
            .WithMany(tp => tp.Shipments)
            .HasForeignKey(s => s.OrderId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ShipmentEntity>()
            .HasOne(s => s.Payment)
            .WithMany(tp => tp.Shipments)
            .HasForeignKey(s => s.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShipmentEntity>()
                .HasOne(s => s.Truck)
                .WithMany(tp =>tp.Shipment)
                .HasForeignKey(s => s.TruckId)
                .OnDelete(DeleteBehavior.Restrict);
          

            modelBuilder.Entity<PaymentEntity>()
                .HasOne(s => s.Order)
                .WithMany(tp => tp.Payments)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            
          

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
         

            // Obtener los paquetes que han sido añadidos, modificados o eliminados
            var packageEntries = ChangeTracker.Entries<PackageEntity>()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
        .ToList();

    // Actualizar el peso total de las órdenes afectadas
    foreach (var packageEntry in packageEntries)
    {
        var package = packageEntry.Entity;

        // Cargar la orden asociada y sus paquetes
        var order = await Orders
            .Include(o => o.Packages)
            .FirstOrDefaultAsync(o => o.Id == package.OrderId, cancellationToken);

        if (order != null)
        {
            // Recalcular el peso total basado en el estado del paquete
            switch (packageEntry.State)
            {
                case EntityState.Added:
                    order.TotalWeight += package.PackageWeight; // Sumar peso
                    break;

                case EntityState.Deleted:
                    order.TotalWeight -= package.PackageWeight; // Restar peso
                    break;

                case EntityState.Modified:

                    // Ajustar peso considerando el cambio en el peso del paquete
                    var originalWeight = packageEntry.OriginalValues.GetValue<double>(nameof(PackageEntity.PackageWeight));
                    var currentWeight = package.PackageWeight;
                    order.TotalWeight += (currentWeight - originalWeight);
                    break;
            }
        }
    }

   

    // Auditar las entidades que heredan de BaseEntity
    var baseEntityEntries = ChangeTracker.Entries<BaseEntity>()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

    foreach (var baseEntityEntry in baseEntityEntries)
    {
        var entity = baseEntityEntry.Entity;
        var userId = _audtiService.GetUserId() ?? "System";

        if (baseEntityEntry.State == EntityState.Added)
        {
            entity.CreatedBy = userId;
            entity.CreatedDate = DateTime.UtcNow;
        }
        else if (baseEntityEntry.State == EntityState.Modified)
        {
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }

    // Llamar al método base para guardar los cambios
    return await base.SaveChangesAsync(cancellationToken);
}



        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<ShipmentEntity> Shipments { get; set; }
        public DbSet<TruckEntity> Trucks { get; set; }

    }
}

