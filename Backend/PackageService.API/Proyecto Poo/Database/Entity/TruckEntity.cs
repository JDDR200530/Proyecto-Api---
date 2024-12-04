using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("trucks", Schema = "dbo")]
    public class TruckEntity
    {
        [Key]
        [Column("truck_id")]
        public Guid TruckId { get; set; }

        [Display(Name = "Se encuentra disponible")]
        [Required]
        [Column("truck_available")]
        public bool TruckAvailable { get; set; }

        [Display(Name = "Capacidad de Carga")]
        [Column("truck_capacity")]
        public double TruckCapacity { get; set; }

       
    }

}
