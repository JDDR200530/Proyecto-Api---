using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("payments", Schema = "dbo")]
    public class PaymentEntity
    {
        [Key]
        [Column("payment_id")]
        public Guid PaymentId { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }
        
        [ForeignKey (nameof(OrderId))]

        public OrderEntity Order { get; set; }

        [Display(Name = "Costo Del Envio")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]
        [StringLength(250)]

        [Column("amount")]
        public double Amount {  get; set; }
      
        [Column("payment_date")]

        [Display(Name = "Metodo de Pago")]
        [Required(ErrorMessage = "El espacio de {0} no puede estar vacio")]

        public DateTime PaymentDate { get; set; }


        [Column("payment_method")]
        [Display(Name = "metodo de pago")]

        public string PaymentMethod { get; set; }

        

        public virtual IEnumerable<ShipmentEntity> Pay { get; set; }


    }
}
