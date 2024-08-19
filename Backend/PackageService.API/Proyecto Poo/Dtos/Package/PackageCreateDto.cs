using Proyecto_Poo.Database.Entity;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Package
{
    public class PackageCreateDto
    {
        public Guid  OrderId { get; set; }
        [Display(Name = "Peso")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public double PackageWeight { get; set; }
    }
}
