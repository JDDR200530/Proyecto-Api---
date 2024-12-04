using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("shipments", Schema = "dbo")]
    public class ShipmentEntity : BaseEntity
    {
        [Column("payment_id")]
        [Required]
        public Guid PaymentId { get; set; }


        [Column("shipment_number")]
        [Display(Name = "Número de Envío")]
        [Required(ErrorMessage = "El número de envío es requerido")]
        [StringLength(50)]
        public string ShipmentNumber { get; set; }


        [ForeignKey(nameof(PaymentId))]
        public virtual PaymentEntity Payment { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual OrderEntity Order { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }


}
