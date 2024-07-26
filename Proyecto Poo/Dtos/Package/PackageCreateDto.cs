using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Package
{
    public class PackageCreateDto
    {
        [Display(Name = "Peso")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public double PackageWeight { get; set; }
    }
}
