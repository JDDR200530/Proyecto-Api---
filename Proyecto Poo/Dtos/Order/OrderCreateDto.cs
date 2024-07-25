using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Order
{
    public class OrderCreateDto
    {
        [Display(Name = "Nombre del remitente")]
        [Required(ErrorMessage ="El {0} es requerido")]
        public string SenderName { get; set; }

        [Display(Name= "Direccion del Paquete" )]
        [Required(ErrorMessage ="La {0} es requerida")]
        public string Address { get; set; }

        [Display(Name = "Nombre del Destinatario")]
        [Required(ErrorMessage ="El {0} es requerido")]
        public string ReciverName { get; set; }

        public DateTime OrderDate { get; set; }


    }
}
