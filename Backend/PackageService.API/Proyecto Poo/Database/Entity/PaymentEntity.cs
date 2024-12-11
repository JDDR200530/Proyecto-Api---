using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("payments", Schema = "dbo")]
    public class PaymentEntity : BaseEntity
    {
        [Display(Name = "Costo Del Envio")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]
        public double Amount { get; set; }

        [Column("payment_date")]
        [Display(Name = "Fecha de Pago")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Column("order_id")]
        public Guid OrderId { get; set; } // Relación con la orden

        [ForeignKey(nameof(OrderId))]
       public virtual OrderEntity Order { get; set; }

        [Display(Name = "Método de Pago")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacío")]
        public string PaymentMethod { get; set; }

        [NotMapped] // No guardar esta propiedad en la base de datos
        public long CardNumber { get; set; }

        public string? PayPalEmail { get; set; }


        public virtual ICollection<ShipmentEntity> Shipments { get; set; } = new List<ShipmentEntity>();
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }



}
