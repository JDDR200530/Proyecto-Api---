using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Order
{
    public class OrderEditDto 
    {
        [Display(Name = "Direccion del Paquete")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public string Address { get; set; }

        [Display(Name = "Nombre del receptor")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string ReceiverName { get; set; }
    }
}
