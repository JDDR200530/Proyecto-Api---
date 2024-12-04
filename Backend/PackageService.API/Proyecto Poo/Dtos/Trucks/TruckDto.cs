using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Proyecto_Poo.Dtos.Order;

namespace Proyecto_Poo.Dtos.Truck
{
    public class TruckDto
    {
        public Guid Id { get; set; }
        public bool TruckAvailable { get; set; }
        public double TruckCapacity { get; set; }

        public List<Guid> OrderIds { get; set; } = new List<Guid>();
    }

}
