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

        [Column("package_id")]
        public Guid PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public virtual PackageEntity Package { get; set; }

        [Column("truck_id")]
        public Guid TruckId { get; set; }
        [ForeignKey(nameof(TruckId))]
        public virtual TruckEntity Truck { get; set; }

        
          public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }


}
