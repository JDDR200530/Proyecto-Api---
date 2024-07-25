using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Dtos.Truck
{
    public class TruckCreateDto
    {



        [Display(Name = "Peso Máximo")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        public double MaxWeight { get; set; }

    }

}
