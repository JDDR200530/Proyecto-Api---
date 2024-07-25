using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("packages", Schema = "dbo")]
    public class PackageEntity
    {
        [Key]
        public Guid PackageId { get; set; }
       
       public Guid OrderId { get; set; }
       [ForeignKey(nameof(OrderId))]

        public OrderEntity Order { get; set; }

        public bool PackageWeight { get; set; }


    }
}
