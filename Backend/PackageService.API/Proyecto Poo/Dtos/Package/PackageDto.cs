using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Dtos.Package
{
    public class PackageDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public double PackageWeight { get; set; }
    }
}
