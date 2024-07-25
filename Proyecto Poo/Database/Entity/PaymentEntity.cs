using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    public class PaymentEntity
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        [ForeignKey (nameof(OrderId))]

        public double Amount {  get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }
    }
}
