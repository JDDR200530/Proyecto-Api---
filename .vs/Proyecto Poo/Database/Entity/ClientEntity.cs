using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    public class ClientEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Display(Name = "mombre")]
        [Required(ErrorMessage ="El {0} del cliente es requerido")]
        [StringLength(50)]
        public string  Name { get; set; }
        [Display(Name = "Direccion")]
        [MinLength(5, ErrorMessage = "La {0} debe tener al menos {1} carateres")]
        [Column("Addres")]
        public string  Address { get; set; }
    }
}
