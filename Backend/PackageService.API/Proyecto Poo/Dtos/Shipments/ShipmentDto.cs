using Proyecto_Poo.Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Shipments
{
    public class ShipmentDto
    { 
           
            public string ShipmentNumber { get; set; }
            public Guid OrderId { get; set; }
           
            public Guid PaymentId { get; set; }
            public Guid TruckId { get; set; }

        public List<Guid> PackageIds { get; set; }

        public string CreatedBy { get; set; }
    }
}
