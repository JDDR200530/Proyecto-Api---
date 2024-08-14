using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Dtos.Package
{
    public class PackageDto
    {
        
        public Guid PackageId { get; set; }

        public double PackageWeight { get; set; }
    }
}
