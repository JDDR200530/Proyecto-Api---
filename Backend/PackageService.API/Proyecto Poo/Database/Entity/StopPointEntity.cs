using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    public class StopPointEntity
    {
        [Key]
        public Guid StopPointId { get; set; }

        [Required]
        [StringLength(100)]
        public string LocationName { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        
        public Guid RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        public virtual RouteEntity Route { get; set; }


    }
}
