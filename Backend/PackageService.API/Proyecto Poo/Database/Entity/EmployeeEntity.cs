using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Database.Entity
{
    public class EmployeeEntity
    {
        [Key]
        [Column("Id")]
        public Guid id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} del empleado es requerido")]
        [StringLength(50)]
        [Column("Name Employee")]
        public string? Name { get; set; }
        [Display(Name = "Cargo")]
        [Required(ErrorMessage = ("El {0} del empleado es requerido"))]
        [Column("Post Employee")]
        public string? Post { get; set; }

        [Display(Name = "Correo Electronio")]
        [MinLength(11)]
        [Required(ErrorMessage = "El {0} del empleado debe tener al menos {1} caracteres")]
        [StringLength(150)]
        [Column("Mail")]
        public string? Mail { get; set; }

        [Display(Name = "Telefono")]
        [MinLength(12)]
        [MaxLength(16)]
        [Required(ErrorMessage ="{El {0} debe tener al menos {1} y no mas de {2} Carateres")]
        [StringLength(16)]
        [Column("Cellphone")]

        public string? Cellphone {  get; set; }

    }
}
