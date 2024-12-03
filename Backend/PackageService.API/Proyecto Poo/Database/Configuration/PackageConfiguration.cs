using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proyecto_Poo.Database.Entity;

namespace Proyecto_Poo.Database.Configuration
{
    public class PackageConfiguration : IEntityTypeConfiguration<PackageEntity>
    {
        public void Configure(EntityTypeBuilder<PackageEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                 .WithMany()
                 .HasForeignKey(e => e.CreatedBy)
                 .HasPrincipalKey(e => e.Id)
                 .IsRequired();

            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasPrincipalKey (e => e.Id)
                .IsRequired();

        }
    }
}
