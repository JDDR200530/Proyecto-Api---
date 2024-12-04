using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("trucks", Schema = "dbo")]
    public class TruckEntity : BaseEntity
    {
      
        [Display(Name = "Se encuentra disponible")]
        [Required]
        [Column("truck_available")]
        public bool TruckAvailable { get; set; }

        [Display(Name = "Capacidad de Carga")]
        [Column("truck_capacity")]
        public double TruckCapacity { get; set; }

        public virtual ICollection<ShipmentEntity> Shipment { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }

    }

}
