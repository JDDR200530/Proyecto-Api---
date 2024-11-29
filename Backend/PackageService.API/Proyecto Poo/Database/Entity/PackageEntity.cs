using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("packages", Schema = "dbo")]
    public class PackageEntity : BaseEntity
    {
        [Column("package_id")]
        public Guid PackageId { get; set; }
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]

        public OrderEntity Order { get; set; }

        [Column("package_weight")]
        [Display(Name = "Peso")]
        [Required(ErrorMessage = "El {0} es requerido")]

        public double PackageWeight { get; set; }
        public virtual IEnumerable<PaymentEntity> Total { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
