using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            // Llamada base necesaria para la configuración de ASP.NET Identity
            base.OnModelCreating(modelBuilder);

            // Configuración de collation
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<OrderEntity>()
                .Property(e => e.SenderName)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.HasDefaultSchema("security");

            // Cambiar los nombres de las tablas por defecto de Identity
       
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");

            // Aplicar la configuración de la entidad de pedido
            modelBuilder.ApplyConfiguration(new OrderCofiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());

            // Configuración global de eliminación en cascada
            var entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in entityTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _audtiService.GetUserId();
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.UpdatedBy = _audtiService.GetUserId();
                        entity.UpdatedDate = DateTime.Now;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<StopPointEntity> StopPoints { get; set; }
        public DbSet<PaymentEntity> Total { get; set; }
        public DbSet<RouteEntity> Routes { get; set; }
        public DbSet<ShipmentEntity> Pay { get; set; }
    }
}
