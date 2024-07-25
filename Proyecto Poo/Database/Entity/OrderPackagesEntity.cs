using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("order_packages", Schema = "dbo")]
    public class OrderPackagesEntity
    {
        [Key]
        [Column("order_id")]
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]

        public OrderEntity Order { get; set; }

        [Column("package_id")]
        public Guid PackageId { get; set; }
        [ForeignKey(nameof(PackageId))]

        public PackageEntity Package { get; set; }

    }
}
