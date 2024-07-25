using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("orders", Schema = "dbo")]
    public class OrderEntity
    {
        [Key]
        [Column("order_id")]

        public Guid OrderId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Nombre del Remitente")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        [StringLength(100)]
        [Column("sender_name")]
        
        public string SenderName { get; set; }
        [Display(Name = "Direccion")]
        [Required(ErrorMessage ="El espacio de {0} no puede estar vacio")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres")]
        [StringLength(250)]
        [Column("address")]
        public string Address { get; set; }

        [Display(Name = "Nombre del Destinatario")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        [StringLength(100)]
        
        [Column("reciver_name")]
        public string ReciverName { get; set; }

        

        public virtual IEnumerable<PackageEntity> Packages { get; set; }
    }
}
