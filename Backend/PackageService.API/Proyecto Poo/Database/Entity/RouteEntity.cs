using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Database.Entity
{
    public class RouteEntity
    {
        [Key]
        public Guid RouteId { get; set; }

        [Required] 
        [StringLength(100)]
        public string RouteName { get; set; }

        public virtual IEnumerable<StopPointEntity> StopPoints { get; set; }
    }
}