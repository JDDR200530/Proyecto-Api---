using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Database.Entity
{
    [Table("packages", Schema = "dbo")]
    public class PackageEntity
    {
        [Key]
        public Guid PackageId { get; set; }
        
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]

        public virtual OrderEntity Order { get; set; }

        public double PackageWeight { get; set; }


    }
}
