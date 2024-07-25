using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Database.Entity
{
    public class TruckEntity
    {
        [Key]
        [Column("truck_id")]
        [Required]
        public Guid TruckId { get; set; }

        [Required]
        [Column("truck_availability")]
        public bool IsAvailable { get; set; }
        [Required]

        [Column("max_weight")]
        public double MaxWeight { get; set; }
    }
}
