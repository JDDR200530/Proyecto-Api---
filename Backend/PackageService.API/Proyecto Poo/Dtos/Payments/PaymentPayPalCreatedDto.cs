namespace Proyecto_Poo.Dtos.Payments
{
    public class PaymentPayPalCreatedDto
    {
        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; } = "PayPal";
        public string PayPalEmail { get; set; }
    }
}
