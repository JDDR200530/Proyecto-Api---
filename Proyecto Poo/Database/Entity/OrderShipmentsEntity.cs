using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("order_shipments", Schema = "dbo")]
    public class OrderShipmentsEntity
    {
        [Key]
        [Column("shipments_id")]
        public Guid OrderShipmentId { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]

       
        public OrderEntity Order { get; set; }

        [Column("shipment_id")]
        public Guid ShipmentId { get; set; }
        [ForeignKey(nameof(ShipmentId))]

        public ShipmentEnity Shipment { get; set; }
       
        
    }
}
