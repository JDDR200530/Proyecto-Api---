using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("orders", Schema = "dbo")]
    public class OrderEntity : BaseEntity
    {
        [Required]
        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Nombre del Remitente")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        [StringLength(300)]
        [Column("sender_name")]
        public string SenderName { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]
        [StringLength(300)]
        [Column("address")]
        public string Address { get; set; }

        [Display(Name = "Nombre del Destinatario")]
        [Required(ErrorMessage = "El {0} del cliente es requerido")]
        [StringLength(300)]
        [Column("receiver_name")]
        public string ReceiverName { get; set; }

        [Column("distance")]
        [Display(Name = "Distancia")]
        [Range(0, double.MaxValue, ErrorMessage = "La distancia debe ser mayor o igual a 0")]
        public double Distance { get; set; }


        [Column("total_weight")]
        [Display(Name = "Peso Total")]
        public double TotalWeight { get; set; }

        [Column("payment_status")]
        [Display(Name = "Estado de Pago")]
        [DefaultValue(false)]
        public bool PaymentStatus { get; set; } = false;

        public virtual ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
        public virtual ICollection<PackageEntity> Packages { get; set; } = new List<PackageEntity>();
        public virtual ICollection<ShipmentEntity> Shipments { get; set; } = new List<ShipmentEntity>();
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }


}
