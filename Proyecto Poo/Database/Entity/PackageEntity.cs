using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("packages", Schema = "dbo")]
    public class PackageEntity
    {
       [Key]
       [Column("package_id")]
       public Guid PackageId { get; set; }

       [Column("order_id")]
        public Guid OrderId { get; set; }
       [ForeignKey(nameof(OrderId))]

        public OrderEntity Order { get; set; }

        [Column("order_id")]
        public bool PackageWeight { get; set; }


    }
}
