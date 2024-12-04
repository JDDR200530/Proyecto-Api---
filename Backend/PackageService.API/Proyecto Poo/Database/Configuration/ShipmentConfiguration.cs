using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Entity;

namespace Proyecto_Poo.Database.Configuration
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<ShipmentEntity>
    {
        public void Configure(EntityTypeBuilder<ShipmentEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                 .WithMany()
                 .HasForeignKey(e => e.CreatedBy)
                 .HasPrincipalKey(e => e.Id)
                 .IsRequired();

            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
        }

    }
}