namespace Proyecto_Poo.Dtos.Payments
{
    public class PaymentCreateDto
    {


        public DateTime PaymentDate { get; set; }

        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; }

        public long CardNumber { get; set; }

        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string CVV { get; set; }

    }
}
