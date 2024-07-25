using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Clientes
{
    public class ClientCreateDto
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Direccion")]
        [MinLength(5, ErrorMessage = "La {0} debe tener al menos {1} carateres")]
        [Column("Address")]
        public string Address { get; set; }
    }
}
