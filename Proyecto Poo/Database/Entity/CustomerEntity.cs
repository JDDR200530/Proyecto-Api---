using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    public class CustomerEntity
    {
        [Key]
        [Column("Id")]
        public Guid id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="El {0} del cliente es requerido")]
        [StringLength(50)]
        [Column("Name Customer")]
        public string?  Name { get; set; }
        [Display(Name = "Direccion")]
        [MinLength(5, ErrorMessage = "La {0} debe tener al menos {1} carateres")]
        [Column("Addres")]
        public string?  Address { get; set; }
    }
}
