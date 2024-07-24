using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Clientes
{
    public class CustomerEditDto
    {
        [Display(Name = "Direccion")]
        [MinLength(5, ErrorMessage = "La {0} debe tener al menos {1} carateres")]
        [Column("Addres")]
        public string Address {  get; set; }
    }
}
