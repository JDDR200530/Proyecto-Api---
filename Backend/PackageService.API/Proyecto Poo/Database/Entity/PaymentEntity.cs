using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("payments", Schema = "dbo")]
    public class PaymentEntity : BaseEntity
    {

        [Column("package_id")]
        public Guid PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual PackageEntity Package { get; set; }

        [Display(Name = "Costo Del Envio")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]
        [StringLength(250)]
        [Column("amount")]
        public double Amount { get; set; }

        [Column("payment_date")]
        [Display(Name = "Fecha de Pago")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]
        public DateTime PaymentDate { get; set; }
        public virtual ICollection<ShipmentEntity> Shipments { get; set; } = new List<ShipmentEntity>();
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }

}
