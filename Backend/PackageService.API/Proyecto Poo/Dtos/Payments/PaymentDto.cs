using Proyecto_Poo.Database.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Payments
{
    public class PaymentDto
    {
       
        public double Amount { get; set; }

       
        public DateTime PaymentDate { get; set; }

        public Guid OrderId { get; set; } 
        public string PaymentMethod { get; set; }

        public long CardNumber { get; set; }


    }
}
