using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("shipments", Schema = "dbo")]
    public class ShipmentEnity
    {
        [Key]
        [Column("shipment_id")]
        public Guid ShipmentId { get; set; }
        
        [Column("payment_id")]
        [Display(Name = "Pago")]

        public Guid PaymentId { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public PaymentEntity Pay { get; set; }



        [Column("truck_available")]
        public Guid TruckId  { get; set; }
        [ForeignKey(nameof(TruckId))]

        public TruckEntity Truck { get; set; }

        [Column("shipped")]
        public bool IsShipped { get; set; }

        public virtual IEnumerable<OrderShipmentsEntity> Shipments { get; set; }



    }
}
