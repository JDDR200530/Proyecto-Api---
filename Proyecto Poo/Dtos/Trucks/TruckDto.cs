using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Truck
{
    public class TruckDto
    {

       
        public Guid TruckId { get; set; }

       
        public bool IsAvailable { get; set; }
        
        public double TruckCapacity { get; set; }
        
    }
}
