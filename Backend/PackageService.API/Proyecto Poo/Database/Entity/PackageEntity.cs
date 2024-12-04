using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("packages", Schema = "dbo")]
    public class PackageEntity : BaseEntity
    {
        [Required]
        [Column("order_id")]
        public Guid OrderId { get; set; } // Clave foránea a Orders.Id

        [Column("package_weight")]
        [Display(Name = "Peso")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public double PackageWeight { get; set; }

        // Relación con Orders
        [ForeignKey(nameof(OrderId))]
        public virtual OrderEntity Order { get; set; } // Propiedad de navegación

        // Propiedades de auditoría

        
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }

 }
