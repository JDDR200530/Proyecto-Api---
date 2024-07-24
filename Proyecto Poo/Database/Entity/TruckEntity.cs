using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Database.Entity
{
    public class TruckEntity
    {
        
        public Guid TruckId { get; set; }

        public bool Available { get; set; }

        public double MaxWeigth { get; set; }
    }
}
