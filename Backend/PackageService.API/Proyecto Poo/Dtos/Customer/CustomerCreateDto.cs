using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Clientes
{
    public class CustomerCreateDto
    {

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Display(Name = "Identidad")]
        [MinLength(13, ErrorMessage = "La {0} debe terner al menos {1} caracteres")]
        [MaxLength(13, ErrorMessage = "La {0} de tener maximo {2} caracteres")]
        [Column("Identity")]
        public int CustomerIdentity { get; set; }
        [Display(Name = "Direccion")]
        [MinLength(5, ErrorMessage = "La {0} debe tener al menos {1} carateres")]
        [Column("Addres")]
        public string CustomerAddress { get; set; }
    }
}
