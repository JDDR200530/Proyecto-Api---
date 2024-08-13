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

        public virtual OrderEntity Order { get; set; }


        [Column("package_weight")]
        [Display (Name = "Peso")]
        [Required(ErrorMessage = "El {0} es requerido")]

        public double PackageWeight { get; set; }



    }
}
